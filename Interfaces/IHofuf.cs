using SqlToMySql.Data.models;

namespace SqlToMySql.Interfaces;

public interface IHofuf{
    Task<int> changeHofuf();
    Task<int> AddProcedure(Class_Procedure cp);
}