using SqlToMySql.Data.models;

namespace SqlToMySql.Implementations;

public class Groningen : IGroningen
{

    private readonly ApplicationDbContext _context;
    public Groningen(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<int> AddProcedure(Class_Procedure cp)
    {
        throw new NotImplementedException();
    }
}