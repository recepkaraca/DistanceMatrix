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
        
        public Task Create()
        {
            _environmentRepository.Create(1);
            return Task.CompletedTask;
        }
    }
}