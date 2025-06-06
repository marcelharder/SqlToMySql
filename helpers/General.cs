using System.Xml.Linq;
using System.Xml.XPath;

namespace SqlToMySql.helpers;

public class General{

    //private readonly XElement _el;
    private readonly IWebHostEnvironment _env;
    private readonly string test;
    private readonly string refPhysPath;
    private readonly string empPath;
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public General(IWebHostEnvironment env, IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("HofufConnection");
        _env = env;
        var content = _env.ContentRootPath;
        var filename = "Data/xml/procedure.xml";
        var refPhysXml = "Data/xml/refPhys.xml";
        var empXml = "Data/xml/employee.xml";
        test = Path.Combine(content, filename);
        refPhysPath = Path.Combine(content, refPhysXml);
        empPath = Path.Combine(content, empXml);
    }
    
    public string GetWeightOfProcedure(int record_id){
        var help = "0";
        
         XElement element = XElement.Load(test);

        var result = (from t in element.Elements("Code")
                     where t.Element("ID").Value == record_id.ToString()
                     select t).SingleOrDefault();

        help = result.Element("weight_of_intervention").Value;
        return help;
    }
    public string GetRefPhysId(string refName){
        var help = "0";
        
         XElement element = XElement.Load(refPhysPath);

        var result = (from t in element.Elements("item")
                     where t.Element("name").Value == refName
                     select t).SingleOrDefault();
        if(result != null){
            help = result.Element("id").Value;
            return help;}
        else {return help;}
        
    }
    public async Task<string> GetEmployeeIdAsync(string refName){ // get this from the hofuf database
        var query = "SELECT id FROM AspNetUsers WHERE UserName = @refName";
        using var connection = new SqlConnection(_connectionString);
        var id = await connection.QueryAsync<string>(query, refName);
        return id.FirstOrDefault();
     }

    public List<AppUser> GetSurgeonsFromXml(int hospital_id)
    {
        XElement element = XElement.Load(empPath);
        var help = "";

        var result = (from t in element.Elements("item")
                      where t.Element("selected_hospital_id").Value == hospital_id.ToString() &&
                            t.Element("profession").Value == "surgeon"
                      select new AppUser
                      {
                          UserName = t.Element("name").Value,
                          worked_in = t.Element("selected_hospital_id").Value,
                          ltk = int.Parse(t.Element("liscense_to_kill").Value)                          

                      }).ToList();
        return result;
    }

    public List<Class_Employee> GetEmployeesFromXml(int hospital_id)
    {
        XElement element = XElement.Load(empPath);

        var result = (from t in element.Elements("item")
                      where t.Element("selected_hospital_id").Value == hospital_id.ToString() &&
                            t.Element("profession").Value != "surgeon"
                      select new Class_Employee
                      {
                          profession = t.Element("profession").Value,
                          selected_hospital_id = t.Element("selected_hospital_id").Value,
                          Id = int.Parse(t.Element("id").Value),
                          image = t.Element("image").Value,   
                          liscense_to_kill = t.Element("liscense_to_kill").Value,
                          name = t.Element("name").Value,
                          password = t.Element("password").Value
                      }).ToList();
        return result;
    }
}