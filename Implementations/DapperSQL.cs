using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using SqlToMySql.Data.models;

namespace SqlToMySql.Implementations;
public class DapperSQL : IDapperSQL
{
private readonly IConfiguration _configuration;
private readonly string _connectionString;
private IMapper _mapper;
public DapperSQL(IConfiguration configuration, IMapper mapper)
{
    _configuration = configuration;
    _connectionString = _configuration.GetConnectionString("HofufConnection");
    _mapper = mapper;
    
}

public async Task<List<Operative>> GetListOfProcedures()
    {
        _ = new List<Operative>();
        _ = new List<Class_Procedure>();
        var query = "Select * FROM dbo.operative";
        using var connection = new SqlConnection(_connectionString);
        var documents = await connection.QueryAsync<Operative>(query);

        List<Operative> result = documents.ToList();

        List<Class_Procedure> ts = getStuffFromOperative(result);

        
        
        
        
        
        
        return result;
    }

    private List<Class_Procedure> getStuffFromOperative(List<Operative> result)
    {
        //fiter this list on m.p. harder
        var help = new List<Class_Procedure>();
        Class_Procedure cp ;
        List<Operative> filteredList = result.Where(h => h.SURGEON_NAME == "M.P. Harder").ToList();

        foreach(Operative op in filteredList){
            
         //get eusur_cabg


         //get procedure_info



        }










        foreach(Operative x in filteredList){
            cp = new Class_Procedure
            {
                //copy the stuff I need from Operative
                ProcedureId = x.PROCEDURE_ID,
                SelectedSurgeon = 5 // dit is dan surgeon code in tracpersonal
            
            
            
            
            };
            help.Add(cp);
            
        }
        return help;
    }

    private int TranslateEmployee(string test){
        var help = 0;
        
        

        return help;
    }







}