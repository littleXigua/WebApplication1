using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {

        private readonly IAspectCoreService _aspectCoreService;
        public ValuesController(IAspectCoreService aspectCoreService  )
        {         
            _aspectCoreService = aspectCoreService;
        }

        [HttpGet()]
        [Route("aspnetcore")]
        public string GetCurrentUtcTime()
        {
           return this._aspectCoreService.GetCurrentUtcTime();
        }
    }
}
