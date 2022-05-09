using Microsoft.AspNetCore.Mvc;

namespace MiniInstagram.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
    }
}
