namespace SqlToMySql.helpers;

public class ComposePatient{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IHofuf _hof;
    Critical_preop_state cps;
    readonly General _gen;
    
   
    
    public ComposePatient(IHofuf hofuf, IConfiguration configuration)
    {
        _hof = hofuf;
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
        
    }

    public async Task<int> AddPatientAsync(int ProcedureId, int PatientId, int record_id){

        Class_Patient patient;
        cps = new Critical_preop_state();
               

        // the data for the patient table comes from 6 tables

        _ = new Euroscore();            Euroscore h5 = await GetEuroScore(ProcedureId);
        _ = new patient_history();      patient_history h6 = await GetPatientHistory(PatientId);
        _ = new eusur_history();        eusur_history h7 = await GetEusurHistory(PatientId);
        _ = new Patient_demographics(); Patient_demographics h8 = await GetPatient_Demographics(PatientId);
        _ = new Cath();                 Cath h9 = await GetCath(PatientId);
        _ = new Operative();            Operative h10 = await GetOperative(ProcedureId);

        cps.Crit_shock = false;
        cps.Crit_inotropes = h6.CV_PREOP_MEDS_INOTROPES == 1;
        cps.Crit_arrythmia = h6.CS_ARRHYTHMIA == "1";
        cps.Crit_resuscitation = h6.CS_RESUSCITATION == "1";
        cps.Crit_iabp = h7.PREOP_IABP == "1";
        cps.Crit_ventilated = h7.PREOP_VENTILATED == "1";
        cps.Crit_renal_failure = h6.RF_RENAL_FAILURE == "1";
        cps.Crit_pacemaker = false;

        patient = new Class_Patient
        {
            MRN = "",
            EuroScoreNo = 0,
            Age = h5.age,
            soort_procedure = record_id,
            Id = 0,
            dead = Convert.ToInt32(h5.dead),
            gender = h8.GENDER,
            extra_cardiac_arteriopathy = h6.RF_PVD,
            poor_mobility = h7.POOR_MOBILITY.ToString(),
            previous_cardiac_surgery = h7.PREVIOUS_CS,
            IsPreviousIntervention = h7.IsPreviousInterventions.ToString(),
            copd = h7.COPD,
            active_endocarditis = h6.RF_INFECTIOUS_ENDOCARD_TYPE.ToString(),

            diabetes_on_insulin = (h6.RF_DIABETES_CONTROL == "1") ? "1" : "2",
            NYHA = h7.DYSPNEA_NYHA.ToString(),
            CCS = h6.CS_CLASS_CCS,
            LVEF = h9.EF.ToString(),
            recent_mi = (h7.PREV_MI_DAYS < 90) ? "1" : "2",
            NOPM = h7.NUMBER_PREV_MI.ToString(),
            systolic_pa_pressure = h9.PA_1.ToString(),
            timing = "",
            reason_urgent = h10.STATUS_URGENT.ToString(),
            reason_emergent = h10.STATUS_EMERGENT.ToString(),
            weight_of_intervention = _gen.GetWeightOfProcedure(record_id),
            surgery_on_thoracic_aorta = "",
            weight = h6.WEIGHT.ToString(),
            height = h6.HEIGHT.ToString(),
            creat_number = 0,
            dialysis = h6.RF_RENAL_FAILURE_DIALYSIS == "1",
            crit_shock = cps.Crit_shock,
            crit_inotropes = cps.Crit_inotropes,
            crit_arrythmia = cps.Crit_arrythmia,
            crit_resuscitation = cps.Crit_resuscitation,
            crit_iabp = cps.Crit_iabp,
            crit_ventilated = cps.Crit_ventilated,
            crit_renal_failure = cps.Crit_renal_failure,
            crit_pacemaker = cps.Crit_pacemaker,
            critical_preoperative_state = DetermineCriticalState(cps),
            log_score = "0"
        };

        await _hof.AddPatient(patient);


        return 1;
    }
     private static bool DetermineCriticalState(Critical_preop_state cs)
    {
        var help = false;
        if (cs.Crit_shock){   help = true;   }
        if (cs.Crit_inotropes){help = true;  }
        if (cs.Crit_arrythmia){help = true;  }
        if (cs.Crit_resuscitation){help = true; }
        if (cs.Crit_iabp){help = true; }
        if (cs.Crit_ventilated){help = true; }
        if (cs.Crit_renal_failure){help = true; }
        if (cs.Crit_pacemaker){help = true; }
        return help;
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
        var result = await connection.QueryAsync<Patient_demographics>(
            query4,
            new { id = PatientId }
        );

        if (result.Any())
        {
            return result.FirstOrDefault();
        }
        else
            return null;
    }
  private async Task<Cath> GetCath(int PatientId)
    {
        var query4 = "select * from cath o where o.PATIENT_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<Cath>(query4, new { id = PatientId });

        if (result.Any())
        {
            return result.FirstOrDefault();
        }
        else
            return null;
    }

 private async Task<Operative> GetOperative(int procedure_id)
    {
        var query4 = "select * from operative o where o.PROCEDURE_ID = @id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<Operative>(query4, new { id = procedure_id });

        if (result.Any())
        {
            return result.FirstOrDefault();
        }
        else
            return null;
    }

}