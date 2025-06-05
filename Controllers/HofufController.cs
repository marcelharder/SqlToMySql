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
    
    [HttpGet ("Go/{hospital_id}")]                
    public async Task<IActionResult> Go(int hospital_id)
    {
        await _dap.CheckSurgeons(hospital_id);
        await _dap.CheckEmployeesAsync(hospital_id);

        var result = await _dap.GetListOfProcedures(hospital_id);
        if (result == null) { return BadRequest("foutje"); }

        return Ok(result);
    }                                   
    
   
}