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

    public async Task<List<Class_Procedure>> GetListOfProcedures(int id){
        var result = await _context.Procedures.FromSqlRaw("SELECT * FROM Procedures WHERE hospital = " + id ).ToListAsync();
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

    public async Task<int> AddPatient(Class_Patient patient)
    {
        _context.Add(patient);
        await _context.SaveChangesAsync();
        return 1;
    }

    public async Task<int> AddValve(Class_Valve cp)
    {
         _context.Add(cp);
        await _context.SaveChangesAsync();
        return 1;
    }

    public async Task<int> AddMinInv(Class_minInv cp)
    {
         _context.Add(cp);
        await _context.SaveChangesAsync();
        return 1;
    }

    public async Task<int> AddPreviewOpReport(Class_Preview_Operative_report cp)
    {
         _context.Add(cp);
        await _context.SaveChangesAsync();
        return 1;
    }

    public async Task<int> GetListOfEmployees(int id)
    {
        var result = await _context.Employees.FromSqlRaw("SELECT * FROM Employees WHERE selected_hospital_id = " + id ).ToListAsync();
        return result.Count;
    }

    public async Task<int> GetListOfUsers(int id)
    {
        var result = await _context.Users.FromSqlRaw("SELECT * FROM AspNetUsers WHERE hospital_id = " + id).ToListAsync();
        return result.Count;
    }

    public async Task<int> AddEmployee(Class_Employee employee)
    {
         _context.Add(employee);
        await _context.SaveChangesAsync();
        return 1;
    }
    public async Task<int>AddSurgeon(AppUser surgeon)
    {
         _context.Add(surgeon);
        await _context.SaveChangesAsync();
        return 1;
    }

    public async Task<int> GetNumberOfProcedure()
    {
        var result = await _context.Procedures.ToListAsync();
        if (result == null)
        {
            return 0;
        }
        return result.Count;
    }
}
