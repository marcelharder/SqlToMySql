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
}
