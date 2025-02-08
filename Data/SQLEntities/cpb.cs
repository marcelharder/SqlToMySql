namespace SqlToMySql.Data.SqlEntities
{
    public class Cpb
    {
        [Key]
        public int PROCEDURE_ID { get; set; }
        public int PATIENT_ID { get; set; }
        public int CROSS_CLAMP_TIME { get; set; }
        public int PERFUSION_TIME { get; set; }
        public int LOWEST_CORE_TEMP { get; set; } = 34;
        public string CARDIOPLEGIA { get; set; }
        public string CARDIOPLEGIA_BLOOD { get; set; }
        public string CARDIOPLEGIA_CRYSTALLOID { get; set; }
        public string CARDIOPLEGIA_O2_CRYSTALLOID { get; set; }
        public int INFUSION_MODE_ANTE { get; set; }
        public int INFUSION_MODE_RETRO { get; set; }
        public int INFUSION_DOSE_INT { get; set; } = 1500;
        public int INFUSION_DOSE_CONT { get; set; }
        public int CARDIOPLEGIA_TEMP_WARM { get; set; }
        public int CARDIOPLEGIA_TEMP_COLD { get; set; }
        public string IABP { get; set; }
        public int IABP_OPTIONS { get; set; }
        public int IABP_IND { get; set; }
        public int PACING_HARV { get; set; }
        public int PACING_ATRIAL { get; set; }
        public int PACING_VENTRICULAR { get; set; }
        public int CARDIOVERSION { get; set; }
        public string VAD { get; set; }
        public int LVAD { get; set; }
        public int RVAD { get; set; }
        public string BVAD { get; set; }
        public string TAH { get; set; }
        public int INOTROPES { get; set; }
        public int Antiarrhythmics { get; set; }
        public int SKIN_INCISION_START_TIME { get; set; }
        public int SKIN_INCISION_STOP_TIME { get; set; }
    }
}




