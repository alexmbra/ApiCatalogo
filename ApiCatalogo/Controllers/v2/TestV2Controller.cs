using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers.v2;

[Produces("application/json")]
[ApiVersion("2.0")]
[Route("api/v{v:apiVersion}/test")]
[ApiController]
public class TestV2Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get2()
    {
        return Content("<html><body><h2>TestV2Controller - V 2.0</h2></body></html>", "text/html");
    }
}
