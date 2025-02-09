using SqlToMySql.Data.SqlEntities;

namespace SqlToMySql.Interfaces;

public interface IDapperSQL{
    Task<List<procedure_info>> GetListOfProcedures();
}