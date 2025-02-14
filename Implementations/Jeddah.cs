using SqlToMySql.Data.models;

namespace SqlToMySql.Implementations;

public class Jeddah : IJeddah
{
     private readonly ApplicationDbContext _context;
    public Jeddah(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> AddProcedure(Class_Procedure cp)
    {
        _context.procedures.Add(cp);
        await _context.SaveChangesAsync();
        return 1;
    }


    public Task<int> changeJeddah()
    {
        throw new NotImplementedException();
    }
}