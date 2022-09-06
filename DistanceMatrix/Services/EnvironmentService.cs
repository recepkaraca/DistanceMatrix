using System.Threading.Tasks;
using DistanceMatrix.Objects;
using DistanceMatrix.Objects.Requests;
using DistanceMatrix.Repositories;

namespace DistanceMatrix.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IEnvironmentRepository _environmentRepository;

        public EnvironmentService(IEnvironmentRepository environmentRepository)
        {
            _environmentRepository = environmentRepository;
        }
        
        public async Task CreateNodes(int nodeCount)
        {
            await _environmentRepository.CreateNodes(nodeCount);
        }

        public async Task CreateRelations(CreateRelationRequest request)
        {
            await _environmentRepository.CreateRelations(request);
        }
        
        public async Task Delete()
        {
            await _environmentRepository.Delete();
        }
        
        public Task<Distance> GetRelation(GetRelationRequest request)
        {
            return _environmentRepository.GetRelation(request);
        }
    }
}