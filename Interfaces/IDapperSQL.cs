using SqlToMySql.Data.SqlEntities;

namespace SqlToMySql.Interfaces;

public interface IDapperSQL{
    Task<List<Operative>> GetListOfProcedures();

    Task<int> CheckForCabg();
    Task<int> CheckForCPB();
}