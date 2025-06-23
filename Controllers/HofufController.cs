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
        await _dap.CheckSurgeons(253);
        await _dap.CheckEmployeesAsync(253);

        var result = new  List<Operative>();

       // result = await _dap.GetListOfProcedures(253);
        if (result == null) { return BadRequest("foutje"); }

        return Ok(result);
    }

}