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

       return Ok(result);
    }
    [HttpGet("CheckCabg")]
    public async Task<IActionResult> GoC()
    {
       var result = await _dap.CheckForCabg();
       if(result == 2){return BadRequest("foutje");}

       return Ok(result);
    }

    [HttpGet("CheckCPB")]
    public async Task<IActionResult> GoCPB()
    {
       var result = await _dap.CheckForCPB();
       if(result == 2){return BadRequest("foutje");}

       return Ok(result);
    }
}