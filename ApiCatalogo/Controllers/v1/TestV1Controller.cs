using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers.v1;

[Produces("application/json")]
[ApiVersion("1.0", Deprecated = true)]
[Route("api/v{v:apiVersion}/test")]
[ApiController]
public class TestV1Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get1()
    {
        return Content("<html><body><h2>TestV1Controller - V 1.0</h2></body></html>", "text/html");
    }
}
