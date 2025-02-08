namespace SqlToMySql.Controllers;

[ApiController]
[Route("[controller]")]
public class JeddahController : ControllerBase
{

    public JeddahController()
    {
        
    }
    
    [HttpGet]
    public IActionResult Go()
    {


        return Ok();
    }
}