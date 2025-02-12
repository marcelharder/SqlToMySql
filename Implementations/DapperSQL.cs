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

      procedure_info p;
        eusur_cabg c;
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

        List<Class_Procedure> ts = await getStuffFromOperativeAsync(result);

        return result;
    }

    private async Task<List<Class_Procedure>> getStuffFromOperativeAsync(List<Operative> result)
    {
        //fiter this list on m.p. harder
        var help = new List<Class_Procedure>();
        Class_Procedure cp;
        List<Operative> filteredList = result.Where(h => h.SURGEON_NAME == "M.P. Harder").ToList();



        foreach (Operative x in filteredList)
        {
            cp = new Class_Procedure
            {
                //copy the stuff I need from Operative
                ProcedureId = x.PROCEDURE_ID,
                SelectedSurgeon = 5, // dit is dan surgeon code in tracpersonal
                DateOfSurgery = await getProcedure(x.PROCEDURE_ID)




            };
            help.Add(cp);

        }
        return help;
    }

    private int TranslateEmployee(string test)
    {
        var help = 0;



        return help;
    }

    private async Task<DateTime> getProcedure(int Procedureid){
   //get procedure_info
            var proc = new List<procedure_info>();
            var query2 = "Select * FROM dbo.procedure_info where PROCEDURE_ID = @id";
            using var connection2 = new SqlConnection(_connectionString);
            var selected_procedure_info = await connection2.QueryAsync<procedure_info>(query2, new { id = Procedureid });
            this.p = selected_procedure_info.First();
            return this.p.SURGERY_DATE;

    }
      private async Task<eusur_cabg> Eusur(int Procedureid){
   //get procedure_info
            var proc = new List<eusur_cabg>();
            var query2 = "Select * FROM dbo.eusur_cabg where PROCEDURE_ID = @id";
            using var connection2 = new SqlConnection(_connectionString);
            var selected_cabg = await connection2.QueryAsync<eusur_cabg>(query2, new { id = Procedureid });
            this.c = selected_cabg.First();
            return this.c;

    }





}