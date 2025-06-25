namespace SqlToMySql.helpers;

public class ComposeCPB{

    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IHofuf _hof;
    
    public ComposeCPB(IHofuf hofuf, IConfiguration configuration)
    {
        _hof = hofuf;
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
    }

    public async Task<int> AddCPBAsync(int Procedureid){
         
        Class_CPB cpbnew;
        _ = new Cpb(); Cpb h3 = await GetCpb(Procedureid);
        _ = new eusur_cpb(); eusur_cpb h4 = await GetEusurCPB(Procedureid);
        cpbnew = new Class_CPB
        {
            PROCEDURE_ID = Procedureid,
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

        return 1;
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


    
}