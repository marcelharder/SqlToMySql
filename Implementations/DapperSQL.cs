using SqlToMySql.Data.SqlEntities;
using SqlToMySql.Data.SQLEntities;
using SqlToMySql.helpers;

namespace SqlToMySql.Implementations;

public class DapperSQL : IDapperSQL
{
    private readonly General _gen;
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IHofuf _hof;
    private readonly IMapper _map;
    private readonly ComposeCPB _cpb;
    private readonly ComposePatient _cp;


    procedure_info p;
    eusur_operative c;
    Queen_support queen_Support;

    public DapperSQL(
        IConfiguration configuration,
        IHofuf hof,
        IMapper map,
        General gen,
        ComposeCPB cpb,
        ComposePatient cp
    )
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
        _hof = hof;
        _map = map;
        _gen = gen;
        _cpb = cpb;
        _cp = cp;
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
        foreach (Operative x in result)
        {
            await GetProceduresAsync(x);
        }

        return result;
    }

    private async Task<int> GetProceduresAsync(Operative x)
    {
        Class_Procedure cp;
        _ = new eusur_operative();
        eusur_operative h1 = await Eusur(x.PROCEDURE_ID);
        _ = new procedure_info();
        procedure_info h2 = await GetProcedure(x.PROCEDURE_ID);
        _ = new Queen_support();
        Queen_support h3 = await Get_Queen_support(x.PROCEDURE_ID);

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
            SelectedTiming = GetProcedureTiming(h1),
            SelectedUrgentTiming = x.STATUS_URGENT,
            SelectedEmergencyTiming = x.STATUS_URGENT,
            SelectedStartHr = h1.Skin_incision_start_hr,
            SelectedStartMin = h1.Skin_incision_start_min,
            SelectedStopHr = h1.Skin_incision_stop_hr,
            SelectedStopMin = h1.Skin_incision_stop_min,
            TotalTime = h1.Total_time,
            SelectedInotropes = Convert.ToInt32(h3.INOTR),
            SelectedPacemaker = Convert.ToInt32(h3.PACEMAKER),
            SelectedPleura = Convert.ToInt32(h3.PLEURA),
            Comment1 = h3.COMMENT_A,
            Comment2 = h3.COMMENT_B,
            Comment3 = h3.COMMENT_C,
        };
        await _hof.AddProcedure(cp);

        await _cpb.AddCPBAsync(x.PROCEDURE_ID);
        await _cp.AddPatientAsync(x.PROCEDURE_ID, (int)h2.PATIENT_ID, h2.record_id);
        await AddCabg(x.PROCEDURE_ID);
        await AddValve(x.PROCEDURE_ID);
        await AddMinInv(x);

        return 1;
    }



    private int TranslateEmployee(string test)
    {
        int help = Convert.ToInt32(_gen.GetEmployeeId(test));
        return help;
    }

    private static int GetProcedureTiming(eusur_operative op)
    {
        var help = 0;
        if (op.status_el == "1")
        {
            help = 1;
        }
        if (op.status_ur == "1")
        {
            help = 2;
        }
        if (op.status_em == "1")
        {
            help = 3;
        }
        if (op.status_salvage == "1")
        {
            help = 4;
        }
        return help;
    }

    private int TranslateCardiologist(string test)
    {
        int help = Convert.ToInt32(_gen.GetRefPhysId(test));
        return help;
    }

    private async Task<int> AddMinInv(Operative x)
    {
        Class_minInv cmin;
        _ = new Class_minInv { };
        cmin = _map.Map<Class_minInv>(x);
        await _hof.AddMinInv(cmin);
        return 1;
    }

    private async Task<int> AddCabg(int procedureId)
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

    private async Task<int> AddValve(int procedureId)
    {
        Class_Valve va;
        var query4 = "select * from valves o where o.PROCEDURE_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<Valves>(query4, new { id = procedureId });

        if (result != null)
        {
            va = new Class_Valve { };
            va = _map.Map<Class_Valve>(result.First());
            await _hof.AddValve(va);
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

    private async Task<Queen_support> Get_Queen_support(int ProcedureId)
    {
        //get eusur_operative
        var query2 = "Select * FROM dbo.queen_support where PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_op = await connection2.QueryAsync<Queen_support>(
            query2,
            new { id = ProcedureId }
        );
        this.queen_Support = selected_op.First();
        return this.queen_Support;
    }


}
