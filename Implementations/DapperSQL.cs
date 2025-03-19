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
        Class_Patient patient;
        // the data for the patient table comes from 5 tables

        var h5 = new Euroscore(); h5 = await this.GetEuroScore(x.PROCEDURE_ID);
        var h6 = new patient_history(); h6 = await this.GetPatientHistory((int)h2.PATIENT_ID);
        var h7 = new eusur_history(); h7 = await this.GetEusurHistory((int)h2.PATIENT_ID);
        var h8 = new Patient_demographics(); h8 = await this.GetPatient_Demographics((int)h2.PATIENT_ID);
       // var h9 = new Cath(); h9 = await this.GetPatient_Demographics((int)h2.PATIENT_ID);
      
        patient = new Class_Patient{
            MRN = "",
            EuroScoreNo = 0,
            Age = h5.age,
            soort_procedure = 0,
            Id = 0,
            dead = Convert.ToInt32(h5.dead),
            gender = h8.GENDER,
            extra_cardiac_arteriopathy = h6.RF_PVD,
            poor_mobility = h7.POOR_MOBILITY.ToString(),
            previous_cardiac_surgery = h7.PREVIOUS_CS,
            IsPreviousIntervention = h7.IsPreviousInterventions.ToString(),
            copd = h7.COPD,
            active_endocarditis = h6.RF_INFECTIOUS_ENDOCARD_TYPE.ToString(),
            critical_preoperative_state = determineCriticalState(),
            diabetes_on_insulin = (h6.RF_DIABETES_CONTROL == "1") ? "1" : "2",
            NYHA = h7.DYSPNEA_NYHA.ToString(),
            CCS = h6.CS_CLASS_CCS,
            LVEF = 




        };

   

    await _hof.AddPatient(patient);

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
            PACING_HARV = h3.PACING_HARV,
            PACING_ATRIAL = h3.PACING_ATRIAL,
            PACING_VENTRICULAR = h3.PACING_VENTRICULAR,
            CARDIOVERSION = h3.CARDIOVERSION,
            VAD = h3.VAD,
            LVAD = h3.LVAD,
            RVAD = h3.RVAD,
            BVAD = h3.BVAD,
            TAH = h3.TAH,
            INOTROPES = h3.INOTROPES,
            Antiarrhythmics = h3.Antiarrhythmics,
            SKIN_INCISION_START_TIME = h3.SKIN_INCISION_START_TIME,
            SKIN_INCISION_STOP_TIME = h3.SKIN_INCISION_STOP_TIME,
            opcab_attempt = h4.opcab_attempt.ToString(),
            cpb_used = h4.cpb_used.ToString(),
            a1 = h4.a1.ToString(),
            a2 = h4.a2.ToString(),
            a3 = h4.a3.ToString(),
            a4 = h4.a4.ToString(),
            v1 = h4.v1.ToString(),
            v2 = h4.v2.ToString(),
            v3 = h4.v3.ToString(),
            v4 = h4.v4.ToString(),
            aoOCCL = h4.aoOCCL.ToString(),
            long_isch = h4.long_isch,
            cardiopl_timing = h4.cardiopl_timing,
            cardiopl_temp = h4.cardiopl_temp,
            cns_time_1 = h4.cns_time_1,
            cns_time_2 = h4.cns_time_2,
            cns_time_3 = h4.cns_time_3,
            deep_hypo = h4.deep_hypo,
            deep_hypo_rcp = h4.deep_hypo_rcp,
            acp_circ = h4.acp_circ,
            other_cns_protect = h4.other_cns_protect,
            nonCMProtect = h4.nonCMProtect,
            nonCMProtect_type = (short)h4.nonCMProtect_type,
            myoplasty = h4.myoplasty,
            cpb_start_hr = h4.cpb_start_hr,
            cpb_stop_hr = h4.cpb_stop_hr,
            cpb_start_min = h4.cpb_start_min,
            cpb_stop_min = h4.cpb_stop_min,
            clamp_start_hr = h4.clamp_start_hr,
            clamp_stop_hr = h4.clamp_stop_hr,
            clamp_start_min = h4.clamp_start_min,
            clamp_stop_min = h4.clamp_stop_min,
            other_cardiac_support = h4.other_cardiac_support,
            cardiac_support = h4.cardiac_support
        };

        await _hof.AddCPB(cpbnew);

        // use the procedure_id to fill CABG
        await GetCabg(x.PROCEDURE_ID);

        return 1;
    }

    private bool determineCriticalState()
    {
        throw new NotImplementedException();
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

    private async Task<int> GetCabg(int procedureId)
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

 private async Task<Euroscore> GetEuroScore(int procedure_id)
    {
        var query4 = "select * from euroscore o where o.PROCEDURE_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<Euroscore>(query4, new { id = procedure_id });

        if (result.Any())
        {
            return result.FirstOrDefault();
        }
        else
            return null;
    }
    private async Task<patient_history> GetPatientHistory(int PatientId)
    {
        var query4 = "select * from patient_history o where o.PATIENT_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<patient_history>(query4, new { id = PatientId });

        if (result.Any())
        {
            return result.FirstOrDefault();
        }
        else
            return null;
    }
    private async Task<eusur_history> GetEusurHistory(int PatientId)
    {
        var query4 = "select * from eusur_history o where o.PATIENT_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<eusur_history>(query4, new { id = PatientId });

        if (result.Any())
        {
            return result.FirstOrDefault();
        }
        else
            return null;
    }
     private async Task<Patient_demographics> GetPatient_Demographics(int PatientId)
    {
        var query4 = "select * from patient_demographics o where o.PATIENT_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<Patient_demographics>(query4, new { id = PatientId });

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
