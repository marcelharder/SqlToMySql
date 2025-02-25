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

    /*   public async Task<int> AddProcedure(Class_Procedure cp)
      {
          // save this new procedure to trac with dapper
          await Task.Run(() =>
          {
              var query =
                  "INSERT INTO Procedures (hospital,refPhys,ImageUrl,YearTaken,Location,Familie,Category,Series,Quality,Spare1,Spare2,Spare3)"
                  + "VALUES(@hospital,@refPhys,@YearTaken,@Location,@Familie,@Category,@Series,@Quality,@Spare1,@Spare2,@Spare3)";

              var parameters = new DynamicParameters();

              parameters.Add("hospital", cp.hospital, DbType.Int32);
              parameters.Add("refPhys", cp.refPhys, DbType.Int32);
              parameters.Add("PatientId", cp.refPhys, DbType.Int32);
              parameters.Add("fdType", cp.refPhys, DbType.Int32);
              parameters.Add("DateOfSurgery", cp.DateOfSurgery, DbType.DateTime);
              parameters.Add("Sequence", cp.Sequence, DbType.String);
              parameters.Add("SelectedSurgeon", cp.SelectedSurgeon, DbType.String);
              parameters.Add("SelectedResponsibleSurgeon", cp.SelectedResponsibleSurgeon, DbType.Int32);

              // tot hire done
              parameters.Add("SelectedAnaesthesist", "n/a", DbType.String);
              parameters.Add("SelectedPerfusionist", "n/a", DbType.String);
              parameters.Add("SelectedNurse1", "n/a", DbType.String);
              parameters.Add("SelectedNurse2", "n/a", DbType.String);
              parameters.Add("Description", "n/a", DbType.String);
              parameters.Add("SurgeryBeforeNextWorkingDay", "n/a", DbType.String);
              parameters.Add("SelectedTiming", "n/a", DbType.String);
              parameters.Add("SelectedUrgentTiming", "n/a", DbType.String);
              parameters.Add("SelectedEmergencyTiming", "n/a", DbType.String);
              parameters.Add("SelectedStartHr", "n/a", DbType.String);
              parameters.Add("SelectedStopHr", "n/a", DbType.String);
              parameters.Add("SelectedStartMin", "n/a", DbType.String);
              parameters.Add("SelectedStopMin", "n/a", DbType.String);
              parameters.Add("TotalTime", "n/a", DbType.String);
              parameters.Add("SelectedInotropes", "n/a", DbType.String);
              parameters.Add("SelectedPericard", "n/a", DbType.String);
              parameters.Add("SelectedPleura", "n/a", DbType.String);
              parameters.Add("Comment1", "n/a", DbType.String);
              parameters.Add("Comment2", "n/a", DbType.String);
              parameters.Add("Comment3", "n/a", DbType.String);


              using (var connection = _context.CreateConnection())
              {
                  var id = connection.Execute(query, parameters);
              }
          });
          return 1;


      } */



    public Task<int> ChangeHofuf()
    {
        throw new NotImplementedException();
    }
}
