namespace SqlToMySql.Controllers;

[ApiController]
[Route("[controller]")]
public class HofufController : ControllerBase
{

    public HofufController()
    {
        
    }
    
    [HttpGet]
    public IActionResult Go()
    {


        return Ok();
    }
}