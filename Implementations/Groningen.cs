using SqlToMySql.Data.models;

namespace SqlToMySql.Implementations;

public class Groningen : IGroningen
{

    private readonly ApplicationDbContext _context;
    public Groningen(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> AddProcedure(Class_Procedure cp)
    {
        _context.procedures.Add(cp);
        await _context.SaveChangesAsync();
        return 1;
    }

  
}