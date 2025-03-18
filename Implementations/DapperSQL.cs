namespace SqlToMySql.Implementations;

public class DapperSQL : IDapperSQL
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IHofuf _hof;
    private readonly IMapper _map;

    procedure_info p;
    eusur_operative c;
    Cpb cpb;
    eusur_cabg ca;
    eusur_cpb eucpb;

    public DapperSQL(IConfiguration configuration, IHofuf hof, IMapper map)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
        _hof = hof;
        _map = map;
    }

    public async Task<List<Operative>> GetListOfProcedures()
    {
        _ = new List<Operative>();
        _ = new List<Class_Procedure>();

        var query =
            "select * from hofuf.dbo.operative o where o.SURGEON_NAME = 'M.P. Harder' or o.ASSISTANT_SURGEON = 'M.P. Harder'";
        using var connection = new SqlConnection(_connectionString);
        var documents = await connection.QueryAsync<Operative>(query);
        List<Operative> result = documents.ToList();
        /*  foreach (Operative s in result)
         {
             await CheckForCabg(s.PROCEDURE_ID);
         } */
        var ts = await getStuffFromOperativeAsync(result);
        return result;
    }

    private async Task<int> GetStuffFromOperativeAsync(List<Operative> result)
    {
        Class_Procedure cp;
        foreach (Operative x in result)
        {
            var h1 = new eusur_operative();
            h1 = await this.Eusur(x.PROCEDURE_ID);
            var h2 = new procedure_info();
            h2 = await this.GetProcedure(x.PROCEDURE_ID);

            cp = new Class_Procedure
            {
                hospital = 253, // is code for hofuf
                Description = h2.fd_TYPE,
                fdType = h2.record_id,
                PatientId = (Int32)h2.PATIENT_ID,
                refPhys = TranslateCardiologist(h2.CARDIOLOGIST),
                SelectedSurgeon = TranslateEmployee(x.SURGEON_NAME),
                SelectedResponsibleSurgeon = TranslateEmployee(x.RESPONSIBLE_FOR_PROC),
                SelectedAnaesthesist = TranslateEmployee(h1.anaesthesist),
                SelectedPerfusionist = TranslateEmployee(h1.perfusionist),
                SelectedAssistant = TranslateEmployee(x.ASSISTANT_SURGEON),
                SelectedNurse1 = TranslateEmployee(h1.nurse_1),
                SelectedNurse2 = x.PROCEDURE_ID,
                DateOfSurgery = h2.SURGERY_DATE,
            };
            await _hof.AddProcedure(cp);
        }

        return 1;
    }

    private static int TranslateEmployee(string test)
    {
        var help = 5;

        return help;
    }

    private static int TranslateCardiologist(string test)
    {
        var help = 99;
        if (test == "Ayman Al Kholeifi")
        {
            help = 2;
        }
        if (test == "Abdullah Al Jubour")
        {
            help = 3;
        }
        if (test == "Abdullah Ghabashi")
        {
            help = 4;
        }

        return help;
    }

    private async Task<int> FindCabg(int procedureId)
    {
        Class_CABG ca;
        var query4 = "select * from eusur_cabg o where o.PROCEDURE_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<eusur_cabg>(query4, new { id = procedureId });

        if (result != null)
        {
            ca = new Class_CABG { };
            ca = _map.Map<Class_CABG>(result.First());
            await _hof.AddCabg(ca);
        }

        return 1;
    }

    private async Task<procedure_info> GetProcedure(int Procedureid)
    {
        //get procedure_info
        var query2 = "Select * FROM dbo.procedure_info where PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_procedure_info = await connection2.QueryAsync<procedure_info>(
            query2,
            new { id = Procedureid }
        );
        this.p = selected_procedure_info.First();
        return this.p;
    }

    private async Task<eusur_operative> Eusur(int Procedureid)
    {
        //get eusur_operative
        var query2 = "Select * FROM dbo.eusur_operative where PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_op = await connection2.QueryAsync<eusur_operative>(
            query2,
            new { id = Procedureid }
        );
        this.c = selected_op.First();
        return this.c;
    }

    private async Task<int> GetCpb(int Procedureid)
    {
        //get cpb
        var query2 = "select * from cpb o where o.PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_op = await connection2.QueryAsync<Cpb>(query2, new { id = Procedureid });
         if(selected_op.Any()){
            cpb = selected_op.FirstOrDefault();
            return 1;
            }
         else{
            return 2;
         }
    }

    public async Task<int> CheckForCabg()
    {
        var list = new List<Class_Procedure>();
        list = await _hof.GetListOfProcedures();
        foreach (Class_Procedure cp in list)
        {
            await FindCabg(cp.SelectedNurse2); // procedureId is temporarily put in SelectedNurse2
        }
        return 1;
    }

    public async Task<int> CheckForCPB()
    {
        var list = new List<Class_Procedure>();
        list = await _hof.GetListOfProcedures();
        foreach (Class_Procedure cp in list)
        {
            await FindCPB(cp.SelectedNurse2);
        }

        return 1;
    }

    private async Task<int> FindCPB(int procedureId)
    {
        Class_CPB clcpb;
       
        var query4 = "select * from eusur_cpb o where o.PROCEDURE_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<eusur_cpb>(query4, new { id = procedureId });

        if (result.Any())
        {
            this.eucpb = result.FirstOrDefault();
            clcpb = new Class_CPB { };
            // get the stuff from eusur_cpb nu
            clcpb.PROCEDURE_ID = procedureId;
            clcpb.cpb_used = ChangeOneToYes(this.eucpb.cpb_used);

            if (await GetCpb(procedureId) != 2) // store the cpb in a local variable
            {
                clcpb.INFUSION_MODE_ANTE = ChangeOneToYes(cpb.INFUSION_MODE_ANTE);

            }
            await _hof.AddCPB(clcpb);
        }

        return 1;
    }

    private static string ChangeOneToYes(int test)
    {
        var result = "No";
        if (test == 1)
        {
            result = "Yes";
        }
        return result;
    }
}
