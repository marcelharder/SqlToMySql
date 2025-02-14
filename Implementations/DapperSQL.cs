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
    private readonly IHofuf _hofuf;

    procedure_info p;
    eusur_operative c;

    public DapperSQL(IConfiguration configuration, IMapper mapper, IHofuf hofuf)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
        _mapper = mapper;
        _hofuf = hofuf;
    }

    public async Task<List<Operative>> GetListOfProcedures()
    {
        _ = new List<Operative>();
        _ = new List<Class_Procedure>();
        var query = "Select * FROM dbo.operative";
        using var connection = new SqlConnection(_connectionString);
        var documents = await connection.QueryAsync<Operative>(query);
        List<Operative> result = documents.ToList();
        var ts = await getStuffFromOperativeAsync(result);
        return result;
    }

    private async Task<int> getStuffFromOperativeAsync(List<Operative> result)
    {
        //fiter this list on m.p. harder
        Class_Procedure cp;
        List<Operative> filteredList = result.Where(h => h.SURGEON_NAME == "M.P. Harder").ToList();

        foreach (Operative x in filteredList)
        {
            var h1 = new eusur_operative(); h1 = await this.Eusur(x.PROCEDURE_ID);
            var h2 = new procedure_info();  h2 = await this.GetProcedure(x.PROCEDURE_ID);

            cp = new Class_Procedure
            {
                //copy the stuff I need from Operative

                ProcedureId = x.PROCEDURE_ID,
                SelectedSurgeon = this.TranslateEmployee(x.SURGEON_NAME),
                SelectedResponsibleSurgeon = this.TranslateEmployee(x.RESPONSIBLE_FOR_PROC),
                SelectedAnaesthesist = this.TranslateEmployee(h1.anaesthesist),
                SelectedPerfusionist = this.TranslateEmployee(h1.perfusionist),
                SelectedAssistant = this.TranslateEmployee(x.ASSISTANT_SURGEON),
                SelectedNurse1 = this.TranslateEmployee(h1.nurse_1),
                SelectedNurse2 = this.TranslateEmployee(h1.nurse_2),
                DateOfSurgery = h2.SURGERY_DATE,
            };
            await _hofuf.AddProcedure(cp);
        }
        return 1;
    }

    private int TranslateEmployee(string test)
    {
        var help = 5;

        return help;
    }

    private async Task<procedure_info> GetProcedure(int Procedureid)
    {
        //get procedure_info
        var query2 = "Select * FROM dbo.procedure_info where PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_procedure_info = await connection2.QueryAsync<procedure_info>(
            query2,
            new { id = Procedureid }
        );
        this.p = selected_procedure_info.First();
        return this.p;
    }

    private async Task<eusur_operative> Eusur(int Procedureid)
    {
        //get eusur_operative
        var query2 = "Select * FROM dbo.eusur_operative where PROCEDURE_ID = @id";
        using var connection2 = new SqlConnection(_connectionString);
        var selected_op = await connection2.QueryAsync<eusur_operative>(
            query2,
            new { id = Procedureid }
        );
        this.c = selected_op.First();
        return this.c;
    }
}
