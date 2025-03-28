namespace SqlToMySql.Data.SqlEntities;

public class Queen_support
{
    [Key]
    public int PROCEDURE_ID { get; set; }
    public string PLEURA { get; set; }
    public string PERICARD { get; set; }
    public string COMMENT_A { get; set; }
    public string COMMENT_B { get; set; }
    public string COMMENT_C { get; set; }
    public string INOTR { get; set; }
    public string IABP { get; set; }
    public string PACEMAKER { get; set; }
    public string CARDIOPLEG { get; set; }
}
