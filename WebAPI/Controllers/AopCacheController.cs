using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AopCacheController : ControllerBase
    {
        private IStudentService _studentservice;
        public AopCacheController(IStudentService service)
        {

            _studentservice = service;
        }


        [HttpGet]
        public async Task<string> Get()
        {
            var students = await _studentservice.GetStudentByIdAsync(6);
            return students.Name ?? "No Name Found";
        }

    }
}
