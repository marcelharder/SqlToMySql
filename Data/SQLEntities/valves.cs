namespace SqlToMySql.Data.SQLEntities;

public class Valves
{
    [Key]
    public int Procedure_id { get; set; }
    public string Description { get; set; }
    public string Vendor_code { get; set; }
    public string Type { get; set; }
    public DateTime Manufac_date { get; set; }
    public DateTime Expiry_date { get; set; }
    public string Serial_no { get; set; }
    public string Model_code { get; set; }
    public string Size { get; set; }
    public string Implant_position { get; set; }
    public string implanted { get; set; }
    public string Hospital_code { get; set; }
    public DateTime Implant_date { get; set; }
}
