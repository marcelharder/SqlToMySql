namespace SqlToMySql.Data.SqlEntities;

public class Cath
{
    [Key]
    public int PATIENT_ID { get; set; }
    public DateTime CATH_DATE { get; set; }
    public string CATH_DURING_ADMISSION { get; set; }
    public string CATH_AT_SAME_HOSPITAL { get; set; }
    public int NUM_DIS_CORON_VESSELS { get; set; }
    public string LEFT_MAIN { get; set; }
    public int HEMO_DATA { get; set; }
    public DateTime HEMO_DATE { get; set; }
    public int EF {get; set;}
}