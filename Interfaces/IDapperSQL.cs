using SqlToMySql.Data.SqlEntities;

namespace SqlToMySql.Interfaces;

public interface IDapperSQL{
    Task CheckEmployeesAsync(int hospital_id);
    Task CheckSurgeons(int hospital_id);
    Task<List<Operative>> GetListOfProcedures();

   
    
}