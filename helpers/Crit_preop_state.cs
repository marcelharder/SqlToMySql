namespace SqlToMySql.helpers;

public class Critical_preop_state{
    public bool Crit_shock { get; set; } = false;
    public bool Crit_inotropes { get; set; } = false;
    public bool Crit_arrythmia { get; set; } = false;
    public bool Crit_resuscitation { get; set; } = false;
    public bool Crit_iabp { get; set; } = false;
    public bool Crit_ventilated { get; set; } = false;
    public bool Crit_renal_failure { get; set; } = false;
    public bool Crit_pacemaker { get; set; } = false;

}