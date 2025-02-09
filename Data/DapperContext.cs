using System.Data;

namespace SqlToMySql.Data;

public class DapperContext
    {
       
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
    }

     public IDbConnection CreateConnection()
         => new MySqlConnection(_connectionString);

    }