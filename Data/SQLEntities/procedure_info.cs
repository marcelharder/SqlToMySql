namespace SqlToMySql.Data.SqlEntities
{
    public class procedure_info
    {
        [Key]
        public int PROCEDURE_ID { get; set; }
        public int Admission_id { get; set; }
        public float PATIENT_ID { get; set; }
        public int record_id { get; set; }
        public string fd_TYPE { get; set; }
        public string PARTICIPANT_ID { get; set; }
        public DateTime SURGERY_DATE { get; set; }
        public int weight_of_intervention { get; set; }
        public string CARDIOLOGIST { get; set; }
        public int SENT { get; set; }
        public int REC_COMP { get; set; }
        public int LOS_ADMIT_SURG { get; set; }
        public int LOS_SURG_DISCHARGE { get; set; }
        public int refcardno { get; set; }
        public string CARDIOLOGIST_CITY { get; set; }
        public string CARDIOLOGIST_PHONE { get; set; }
        public string PHYSICIAN { get; set; }
        public string PHYSICIAN_CITY { get; set; }
        public string REFERRING_PHYSICIAN_PHONE { get; set; }
        public string VERSION { get; set; }
        public string STS_DATA_VERSION { get; set; }
        public string SURGERY_DATE_STRING { get; set; }
    }
}
