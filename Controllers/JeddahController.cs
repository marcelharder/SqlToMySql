namespace SqlToMySql.Controllers;

[ApiController]
[Route("[controller]")]
public class JeddahController : ControllerBase
{
    private readonly IDapperSQL _dap;

    public JeddahController(IDapperSQL dap)
    {
        _dap = dap;
    }

    [HttpGet]
    public async Task<IActionResult> Go()
    {
        await _dap.CheckSurgeons(34);
        await _dap.CheckEmployeesAsync(34);

       /*  var result = await _dap.GetListOfProcedures(34);
        if (result == null)
        {
            return BadRequest("foutje");
        } */

        return Ok();
    }
}
