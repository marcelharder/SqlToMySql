using SqlToMySql.Data.models;

namespace SqlToMySql.Interfaces;

public interface IHofuf{
    Task<int> ChangeHofuf();
    Task<int> AddProcedure(Class_Procedure cp);
}