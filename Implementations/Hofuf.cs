using SqlToMySql.Data.models;

namespace SqlToMySql.Implementations;

public class Hofuf : IHofuf
{
     private readonly ApplicationDbContext _context;
    public Hofuf(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> AddProcedure(Class_Procedure cp)
    {
         _context.procedures.Add(cp);
        await _context.SaveChangesAsync();
        return 1;
    }


    public Task<int> changeHofuf()
    {
        throw new NotImplementedException();
    }
}