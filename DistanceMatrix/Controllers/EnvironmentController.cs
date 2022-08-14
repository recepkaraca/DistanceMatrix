using System.Threading.Tasks;
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
        public async Task<IActionResult> Create(int nodeCount)
        {
            await _environmentService.Create(nodeCount);
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