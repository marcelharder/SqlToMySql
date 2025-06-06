using Microsoft.AspNetCore.Mvc;
using SqlToMySql.helpers;

namespace SqlToMySql.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GeneralController : ControllerBase
{
    private readonly General _gen;
    public GeneralController(General gen)
    {
        _gen = gen;
    }

    [HttpGet("{soort}")]
    public IActionResult GetWeigthOfIntervention(int soort){
        var r = _gen.GetWeightOfProcedure(soort);
        return Ok(r);

    }
    [HttpGet("getRefPhysID/{name}")]
    public IActionResult GetRefPhys(string name){
        var r = _gen.GetRefPhysId(name);
        return Ok(r);

    }
    [HttpGet("getEmployeeID/{name}")]
    public IActionResult GetEmployee(string name){
        var r = _gen.GetEmployeeIdAsync(name); 
        return Ok(r);

    }
}
