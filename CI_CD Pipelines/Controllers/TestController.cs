using Microsoft.AspNetCore.Mvc;

namespace CI_CD_Pipelines.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    
    [HttpGet("int")]
    public ActionResult<int> IntFunc()
    {
        return Ok(13);
    }
    [HttpGet("string")]
    public ActionResult<string> StringFunc()
    {
        return Ok("Ziad");
    }
}