using Dapper;
using Microsoft.EntityFrameworkCore;

namespace SqlToMySql.Implementations;
public class DapperSQL : IDapperSQL
{
private readonly DapperContext _context;
private readonly ApplicationDbContext _db;
public DapperSQL(DapperContext context, ApplicationDbContext db)
{
    _context = context;
    _db = db;
}

public async Task<List<procedure_info>> GetListOfProcedures()
    {
        var query = "Select * FROM dbo.procedure_info";
        using var connection = _context.CreateConnection();
        var documents = await connection.QueryAsync<procedure_info>(query);

        return documents.ToList(); 
     //   var result = await _db.Procedure_infos.ToListAsync();
     //   return result;




    }

   
}