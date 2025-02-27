using SqlToMySql.Data.models;

namespace SqlToMySql.Interfaces;

public interface IHofuf
{
    Task<int> ChangeHofuf();
    Task<int> AddProcedure(Class_Procedure cp);
    Task<int> AddCabg(Class_CABG cp);
    Task<int> AddCPB(Class_CPB cp);
    Task<List<Class_Procedure>> GetListOfProcedures();
}
