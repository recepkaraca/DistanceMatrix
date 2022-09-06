using System.Threading.Tasks;
using DistanceMatrix.Objects.Requests;
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
        
        [HttpPost("nodes")]
        public async Task<IActionResult> CreateNodes(int nodeCount)
        {
            await _environmentService.CreateNodes(nodeCount);
            return Ok();
        }
        
        [HttpPost("relations")]
        public async Task<IActionResult> CreateNodes(CreateRelationRequest request)
        {
            await _environmentService.CreateRelations(request);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            _environmentService.Delete();
            return Ok();
        }
        
        [HttpGet("relation")]
        public async Task<IActionResult> GetRelation([FromQuery] GetRelationRequest request)
        {
            var distance = await _environmentService.GetRelation(request);
            return Ok(distance);
        }
    }
}