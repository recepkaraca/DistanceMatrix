using DistanceMatrix.Services;
using Microsoft.AspNetCore.Mvc;

namespace DistanceMatrix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvironmentController  : ControllerBase
    {
        private readonly IEnvironmentService _environmentService;

        public EnvironmentController(IEnvironmentService environmentService)
        {
            _environmentService = environmentService;
        }
        
        [HttpPost]
        public IActionResult Create()
        {
            _environmentService.Create();
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Delete()
        {
            _environmentService.Delete();
            return Ok();
        }
    }
}