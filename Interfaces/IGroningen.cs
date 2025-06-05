namespace SqlToMySql.Interfaces;

public interface IGroningen{
    Task<int> AddProcedure(Class_Procedure cp);
}