using System.Xml.Linq;
using System.Xml.XPath;

namespace SqlToMySql.helpers;

public class General{

    private readonly XElement _el;
    private readonly IWebHostEnvironment _env;
    private readonly string test;
    public General(IWebHostEnvironment env)
    {
        _env = env;
        var content = _env.ContentRootPath;
        var filename = "Data/xml/procedure.xml";
        test = Path.Combine(content, filename);
        
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
}