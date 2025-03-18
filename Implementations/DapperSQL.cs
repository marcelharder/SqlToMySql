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
        foreach (Operative x in result)
        {
            await GetProceduresAsync(x);
        }

        return result;
    }

    private async Task<int> GetProceduresAsync(Operative x)
    {
        Class_Procedure cp;
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

        // use the patient_id to fill Patients
        FillPatients(cp.PatientId);

        // use the procedure_id to fill CPB
        Class_CPB cpbnew;
        _ = new Cpb();
        Cpb h3 = await GetCpb(x.PROCEDURE_ID);
        _ = new eusur_cpb();
        eusur_cpb h4 = await GetEusurCPB(x.PROCEDURE_ID);
        cpbnew = new Class_CPB
        {
            PROCEDURE_ID = x.PROCEDURE_ID,
            CROSS_CLAMP_TIME = h3.CROSS_CLAMP_TIME,
            PERFUSION_TIME = h3.PERFUSION_TIME,
            LOWEST_CORE_TEMP = h3.LOWEST_CORE_TEMP,
            CARDIOPLEGIA = h3.CARDIOPLEGIA,
            CARDIOPLEGIA_TYPE = (h3.CARDIOPLEGIA_BLOOD == "1") ? "2" : "1",
            INFUSION_MODE_ANTE = h3.INFUSION_MODE_ANTE.ToString(),
            INFUSION_MODE_RETRO = 0,
            INFUSION_DOSE_INT = h3.INFUSION_DOSE_INT,
            INFUSION_DOSE_CONT = h3.INFUSION_DOSE_CONT,
            CARDIOPLEGIA_TEMP_COLD = h3.CARDIOPLEGIA_TEMP_COLD,
            CARDIOPLEGIA_TEMP_WARM = h3.CARDIOPLEGIA_TEMP_WARM,
            IABP = h3.IABP,
            IABP_DATE = h4.IABP_DATE,
            IABP_IND = h3.IABP_IND.ToString(),
            IABP_OPTIONS = h3.IABP_OPTIONS.ToString(),
            
        };

        await _hof.AddCPB(cpbnew);

        // use the procedure_id to fill CABG

        await FindCabg(x.PROCEDURE_ID);

        return 1;
    }

    private void FillPatients(int PatientId) { }

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

    private async Task<Cpb> GetCpb(int Procedureid)
    {
        //get cpb
        var query2 = "select * from cpb o where o.PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_op = await connection2.QueryAsync<Cpb>(query2, new { id = Procedureid });
        if (selected_op.Any())
        {
            return selected_op.FirstOrDefault();
        }
        else
        {
            return null;
        }
    }

    /*  public async Task<int> CheckForCabg()
     {
         var list = new List<Class_Procedure>();
         list = await _hof.GetListOfProcedures();
         foreach (Class_Procedure cp in list)
         {
             await FindCabg(cp.SelectedNurse2); // procedureId is temporarily put in SelectedNurse2
         }
         return 1;
     } */

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

    private async Task<eusur_cpb> GetEusurCPB(int procedureId)
    {
        var query4 = "select * from eusur_cpb o where o.PROCEDURE_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<eusur_cpb>(query4, new { id = procedureId });

        if (result.Any())
        {
            return result.FirstOrDefault();
        }
        else
            return null;
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
