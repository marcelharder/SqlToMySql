using SqlToMySql.Data.SqlEntities;

namespace SqlToMySql.Interfaces;

public interface IDapperSQL{
    Task<List<Operative>> GetListOfProcedures();
}