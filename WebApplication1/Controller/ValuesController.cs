using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("Getstring")]
        [Route("get")]
        [Authorize(Roles = "Admin")]
        public string Get()
        {
            return "Hello World";
        }

        public async Task<IActionResult> GetAsync()
        {
            // Simulate some asynchronous work
            await Task.Delay(1000);
            return Ok("Hello World from async method");
        }
    } 
}
