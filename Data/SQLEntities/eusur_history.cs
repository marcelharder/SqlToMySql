namespace SqlToMySql.Data.SqlEntities
{
    public class eusur_history
    {

        [Key]
        public int PATIENT_ID { get; set; }
        public int SYMPTOM_STATUS { get; set; }
        public int POOR_MOBILITY { get; set; }
        public int DYSPNEA_NYHA { get; set; }
        public int CHF { get; set; }
        public int NUMBER_PREV_MI { get; set; }
        public int OTHER_NONCARDIAC_TYPE { get; set; }
        public int PREV_MI_HOURS { get; set; }
        public int PREV_MI_DAYS { get; set; }
        public string PREOP_INOTROPES { get; set; }
        public string PREOP_VENTILATED { get; set; }
        public string PREOP_PM { get; set; }
        public int PREOP_PM_TYPE { get; set; }
        public string PREOP_IABP { get; set; }
        public string FAILED_INTERVENTION { get; set; }
        public string FAILED_INTERVENTION_TIME { get; set; }
        public float LAST_CREATININE { get; set; }
        public float LAST_SODIUM { get; set; }
        public float LAST_PTT { get; set; }
        public string LIVER_DISEASE { get; set; }
        public string CEREBROVASCULAR_DISEASE { get; set; }
        public int CVA_MONTHS { get; set; }
        public int CVA_WEEKS { get; set; }
        public int TIA_MONTHS { get; set; }
        public int TIA_WEEKS { get; set; }
        public int CAROTID_PERCENTAGE { get; set; }
        public string OTHER_CENTRAL_NEURO { get; set; }
        public string OTHER_PERIPH_NEURO { get; set; }
        public string COPD { get; set; }
        public string STEROID_USE { get; set; }
        public string ASTHMA { get; set; }
        public string BRONCHO_USE { get; set; }
        public int RENAL_FAILURE_TYPE { get; set; }
        public int COPD_SEVERITY { get; set; }
        public int CERVASC_DIS_TYPE { get; set; }
        public string PREVIOUS_CS { get; set; }
        public string PREVIOUS_CARD { get; set; }
        public string BLOODGROUP { get; set; }
        public string RHESUS { get; set; }
        public int IsInterventionalCardiology { get; set; }
        public int IsPreviousInterventions { get; set; }
    }
}

