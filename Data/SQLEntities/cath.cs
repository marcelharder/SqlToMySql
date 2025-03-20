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
    public string HEMO_DATA { get; set; }
    public DateTime HEMO_DATE { get; set; }
    public int EF { get; set; }
    public int EF_METHOD { get; set; }
    public float CO { get; set; }
    public float CI { get; set; }
    public int LVEDP { get; set; }
    public int PA_1 { get; set; }
    public int PA_2 { get; set; }
    public int PAM { get; set; }
    public int PAW { get; set; }
    public int AORTIC_DISEASE { get; set; }
    public string AORTIC_STENOSIS { get; set; }
    public int AORTIC_INSUFFICIENCY { get; set; }
    public int AORTIC_ETIOLOGY { get; set; }
    public int AORTIC_GRADIENT { get; set; }
    public int MITRAL_DISEASE { get; set; }
    public string MITRAL_STENOSIS { get; set; }
    public int MITRAL_INSUFFICIENCY { get; set; }
    public int MITRAL_ETIOLOGY { get; set; }
    public int MITRAL_GRADIENT { get; set; }
    public int TRICUSPID_DISEASE { get; set; }
    public string TRICUSPID_STENOSIS { get; set; }
    public int TRICUSPID_INSUFFICIENCY { get; set; }
    public int TRICUSPID_ETIOLOGY { get; set; }
    public int TRICUSPID_GRADIENT { get; set; }
    public int PULMONIC_DISEASE { get; set; }
    public string PULMONIC_STENOSIS { get; set; }
    public int PULMONIC_INSUFFICIENCY { get; set; }
    public int PULMONIC_ETIOLOGY { get; set; }
    public int PULMONIC_GRADIENT { get; set; }
    public int INTVL_OR { get; set; }
    public int HEMO_DATA_EF_DONE { get; set; }
    public int HDPA_MEAN_DONE { get; set; }
    public int LV_FUNCTION { get; set; }
    public string coronaries_done { get; set; }
    public string ef_done { get; set; }
    public string lvedp_done { get; set; }
    public string rightheart_done { get; set; }
    public string cath_date_string { get; set; }
    public string pasys_done { get; set; }
    public string pamean_done { get; set; }
    public string pawp_done { get; set; }
}
