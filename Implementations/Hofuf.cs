using System.Data;
using Dapper;
using SqlToMySql.Data.models;

namespace SqlToMySql.Implementations;

public class Hofuf : IHofuf
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public Hofuf(IConfiguration configuration,ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<List<Class_Procedure>> GetListOfProcedures(){
        var result = await _context.Procedures.ToListAsync();
        return result;
    }

    public async Task<int> AddProcedure(Class_Procedure cp)
    {
        _context.Add(cp);
        await _context.SaveChangesAsync();
        return 1;

    }
    public async Task<int> AddCabg(Class_CABG cp)
    {
        _context.Add(cp);
        await _context.SaveChangesAsync();
        return 1;

    }

    public Task<int> ChangeHofuf()
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddCPB(Class_CPB cp)
    {
         _context.Add(cp);
        await _context.SaveChangesAsync();
        return 1;
    }
}
