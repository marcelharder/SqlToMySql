namespace SqlToMySql.Data.SqlEntities;

public class Euroscore
{
    [Key]
    public int procedure_id { get; set; }
    public int age { get; set; }
    public string add_score { get; set; }
    public string log_score { get; set; }
    public string surgeon_name { get; set; }
    public string dead { get; set; }
}






