namespace SqlToMySql.Data.SqlEntities;

public class eusur_cpb
{
    [Key]
    public int PROCEDURE_ID { get; set; }
    public int PATIENT_ID { get; set; }
    public int opcab_attempt { get; set; }
    public int cpb_used { get; set; }
    public int a1 { get; set; }
    public int a2 { get; set; }
    public int a3 { get; set; }
    public int a4 { get; set; }
    public int v1 { get; set; }
    public int v2 { get; set; }
    public int v3 { get; set; }
    public int v4 { get; set; }
    public int aoOCCL { get; set; }
    public int long_isch { get; set; }
    public string blood_perfusion { get; set; }
    public string cardiopl_timing { get; set; }
    public string cardiopl_temp { get; set; }
    public string cns_protect { get; set; }
    public int cns_time_1 { get; set; }
    public int cns_time_2 { get; set; }
    public int cns_time_3 { get; set; }
    public string deep_hypo { get; set; }
    public string deep_hypo_rcp { get; set; }
    public string acp_circ { get; set; }
    public string other_cns_protect { get; set; }
    public string nonCMProtect { get; set; }
    public int nonCMProtect_type { get; set; }
    public DateTime IABP_DATE { get; set; }
    public string myoplasty { get; set; }
    public int cpb_start_hr { get; set; }
    public int cpb_start_min { get; set; }
    public int cpb_stop_hr { get; set; }
    public int cpb_stop_min { get; set; }
    public int clamp_start_hr { get; set; }
    public int clamp_start_min { get; set; }
    public int clamp_stop_hr { get; set; }
    public int clamp_stop_min { get; set; }
    public string other_cardiac_support { get; set; }
    public string cardiac_support { get; set; }

}





