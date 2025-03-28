using System.Xml.Linq;
using System.Xml.XPath;

namespace SqlToMySql.helpers;

public class General{

    private readonly XElement _el;
    private readonly IWebHostEnvironment _env;
    private readonly string test;
    private readonly string refPhysPath;
    private readonly string empPath;
    public General(IWebHostEnvironment env)
    {
        _env = env;
        var content = _env.ContentRootPath;
        var filename = "Data/xml/procedure.xml";
        var refPhysXml = "Data/xml/procedure.xml";
        var empXml = "Data/xml/procedure.xml";
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

        help = result.Element("id").Value;
        return help;
    }
    public string GetEmployeeId(string refName){
        var help = "0";
        
         XElement element = XElement.Load(empPath);

        var result = (from t in element.Elements("item")
                     where t.Element("name").Value == refName
                     select t).SingleOrDefault();

        help = result.Element("id").Value;
        return help;
    }
}