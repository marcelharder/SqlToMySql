namespace SqlToMySql.Data.SqlEntities
{
    public class eusur_operative
    {
        [Key]
        public int Procedure_id { get; set; }
        public int Skin_incision_start_hr { get; set; }
        public int Skin_incision_stop_hr { get; set; }
        public int Skin_incision_start_min { get; set; }
        public int Skin_incision_stop_min { get; set; }
        public int Total_time { get; set; }
        public string Surgery_before_next_working_day { get; set; }
        public string anaesthesist { get; set; }
        public string perfusionist { get; set; }
        public string nurse_1 { get; set; }
        public string nurse_2 { get; set; }
        public string free_text { get; set; }
        public string status_el { get; set; }
        public string status_ur { get; set; }
        public string status_em { get; set; }
        public string status_salvage { get; set; }
        public string sequence { get; set; }

    }
}





