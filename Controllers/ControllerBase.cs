using Microsoft.AspNetCore.Mvc;

namespace SimpleTodoApi.Controllers
{
 [Route("api/v{version:apiVersion}/[controller]")]
 [ApiController]
 public class ApiControllerBase : ControllerBase
 {
 }
}
