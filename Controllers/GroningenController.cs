namespace SqlToMySql.Controllers;

[ApiController]
[Route("[controller]")]
public class GroningenController : ControllerBase
{

    public GroningenController()
    {
        
    }
    
    [HttpGet]
    public IActionResult Go()
    {


        return Ok();
    }
}