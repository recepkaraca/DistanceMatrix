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
        
        public async Task Create()
        {
            await _environmentRepository.Create(1);
        }
        
        public async Task Delete()
        {
            await _environmentRepository.Delete();
        }
    }
}