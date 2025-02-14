using SqlToMySql.Data.models;

namespace SqlToMySql.Interfaces;

public interface IJeddah{
    Task<int> changeJeddah();
    Task<int> AddProcedure(Class_Procedure cp);
}