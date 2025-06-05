using SqlToMySql.Data.models;

namespace SqlToMySql.Interfaces;

public interface IHofuf
{
    Task<int> ChangeHofuf();
    Task<int> AddProcedure(Class_Procedure cp);
    Task<int> AddPatient(Class_Patient patient);
    Task<int> AddCabg(Class_CABG cp);
    Task<int> AddCPB(Class_CPB cp);
    Task<int> AddValve(Class_Valve cp);
    Task<int> AddMinInv(Class_minInv cp);
    Task<List<Class_Procedure>> GetListOfProcedures(int hospital_id);
    Task<int> GetListOfEmployees(int hospital_id);
    Task<int> GetListOfUsers(int hospital_id);
    Task<int> AddPreviewOpReport(Class_Preview_Operative_report cp);
    Task<int> AddEmployee(Class_Employee employee);
    Task<int> AddSurgeon(AppUser surgeon);
    Task<int> GetNumberOfProcedure();
}
