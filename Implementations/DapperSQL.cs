using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using SqlToMySql.Data.models;

namespace SqlToMySql.Implementations;

public class DapperSQL : IDapperSQL
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IHofuf _hof;
    private readonly IMapper _map;

    procedure_info p;
    eusur_operative c;

    public DapperSQL(IConfiguration configuration, IHofuf hof, IMapper map)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
        _hof = hof;
        _map = map;
    }

    public async Task<List<Operative>> GetListOfProcedures()
    {
        _ = new List<Operative>();
        _ = new List<Class_Procedure>();

        var query = "select * from hofuf.dbo.operative o where o.SURGEON_NAME = 'M.P. Harder' or o.ASSISTANT_SURGEON = 'M.P. Harder'";
        using var connection = new SqlConnection(_connectionString);
        var documents = await connection.QueryAsync<Operative>(query);
        List<Operative> result = documents.ToList();
        var ts = await getStuffFromOperativeAsync(result);
        return result;
    }

    private async Task<int> getStuffFromOperativeAsync(List<Operative> result)
    {
        Class_Procedure cp;
        foreach (Operative x in result)
        {
            //await CheckForCabg(x.PROCEDURE_ID);
            var h1 = new eusur_operative(); h1 = await this.Eusur(x.PROCEDURE_ID);
            var h2 = new procedure_info();  h2 = await this.GetProcedure(x.PROCEDURE_ID);

            cp = new Class_Procedure
            {
                hospital = 253, // is code for hofuf
                Description = h2.fd_TYPE,
                fdType = h2.record_id,
                PatientId = (Int32) h2.PATIENT_ID,
                refPhys = TranslateCardiologist(h2.CARDIOLOGIST),
                SelectedSurgeon = TranslateEmployee(x.SURGEON_NAME),
                SelectedResponsibleSurgeon = TranslateEmployee(x.RESPONSIBLE_FOR_PROC),
                SelectedAnaesthesist = TranslateEmployee(h1.anaesthesist),
                SelectedPerfusionist = TranslateEmployee(h1.perfusionist),
                SelectedAssistant = TranslateEmployee(x.ASSISTANT_SURGEON),
                SelectedNurse1 = TranslateEmployee(h1.nurse_1),
                SelectedNurse2 = TranslateEmployee(h1.nurse_2),
                DateOfSurgery = h2.SURGERY_DATE,
                
            };
            await _hof.AddProcedure(cp);
        }
        

        return 1;
    }

    private static int TranslateEmployee(string test)
    {
        var help = 5;

        return help;
    }

      private static int TranslateCardiologist(string test)
    {
       var help = 99;
       if(test == "Ayman Al Kholeifi"){help = 2;}
       if(test == "Abdullah Al Jubour"){help = 3;}
       if(test == "Abdullah Ghabashi"){help = 4;}
        

        return help;
    }

    private async Task<int> CheckForCabg(int procedureId){ // is there a cabg record than copy this rocord to trac
        var query3 = "Select * FROM dbo.eusur_cabg where PROCEDURE_ID = @id";
        using var connection3 = new SqlConnection(_connectionString);
        var selected_op = await connection3.QueryAsync<eusur_cabg>(
            query3,
            new { id = procedureId }
        );
        if(selected_op.First() != null){
            CopyCabgToTrac(selected_op.First());
        };
       return 2;
    }

    private void CopyCabgToTrac(eusur_cabg ec)
    {
        _hof.AddCabg(_map.Map<Class_CABG>(ec));
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
