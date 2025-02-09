using System.Data;
using System.Threading.Tasks;

namespace SqlToMySql.Controllers;

[ApiController]
[Route("[controller]")]
public class HofufController : ControllerBase
{
     private readonly IDapperSQL _dap;

    public HofufController(IDapperSQL dap)
    {
        _dap = dap;
    }
    
    [HttpGet]
    public async Task<IActionResult> Go()
    {
       var result = await _dap.GetListOfProcedures();
       if(result == null){return BadRequest("foutje");}

        return Ok("Klaar");
    }
}