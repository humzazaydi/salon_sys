using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Authorize(Roles = "Officer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("GetValues")]
        public IActionResult Get()
        {
            List<string> values = new List<string>();
            values.Add("value1");
            values.Add("value2");
            return Ok(values);
        }
    }
}
