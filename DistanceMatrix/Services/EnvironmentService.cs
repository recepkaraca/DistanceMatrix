using System;
using System.Threading.Tasks;
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
        
        public async Task Create(int nodeCount)
        {
            await _environmentRepository.Create(nodeCount);
        }
        
        public async Task Delete()
        {
            await _environmentRepository.Delete();
        }
    }
}