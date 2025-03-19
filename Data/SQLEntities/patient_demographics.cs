namespace SqlToMySql.Data.SqlEntities;

public class Patient_demographics
{
    [Key]
    public int PATIENT_ID { get; set; }
    public string LAST_NAME { get; set; }
    public string FIRST_NAME { get; set; }
    public string MIDDLE_INITIAL { get; set; }
    public string MED_REC_NUMBER { get; set; }
    public string ADDRESS1 { get; set; }
    public string CITY { get; set; }
    public string STATE { get; set; }
    public string ZIP { get; set; }
    public string PHONE { get; set; }
    public string MOBILE_PHONE { get; set; }
    public string SSNO { get; set; }
    public DateTime DOB { get; set; }
    public string GENDER { get; set; }
    public int RACE { get; set; }
    public int BIRTH_COUNTRY { get; set; }
    public string DOB_STRING { get; set; }
}
