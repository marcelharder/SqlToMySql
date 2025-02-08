namespace SqlToMySql.Data.SqlEntities
{
    public class aortic_surgery
    {
        [Key]
        public int procedure_id { get; set; }
        public int patient_id { get; set; }
        public string aneurysm { get; set; }
        public string aneurysm_type { get; set; }
        public string dissection { get; set; }
        public string dissection_onset { get; set; }
        public string dissection_type { get; set; }
        public string coarctation { get; set; }
        public string other_congenital { get; set; }
        public string pathology { get; set; }
        public string indication { get; set; }
        public string operative_technique { get; set; }
        public string range { get; set; }
        public string stent_graft_technique { get; set; }
    }
}









