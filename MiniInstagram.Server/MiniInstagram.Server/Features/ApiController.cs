using Microsoft.AspNetCore.Mvc;

namespace MiniInstagram.Server.Features
{
    [Route("[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
    }
}
