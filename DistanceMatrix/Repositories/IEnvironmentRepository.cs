using System.Threading.Tasks;

namespace DistanceMatrix.Repositories
{
    public interface IEnvironmentRepository
    {
        Task<int> Create(long nodeCount);
    }
}