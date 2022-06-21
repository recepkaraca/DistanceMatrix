using System.Threading.Tasks;

namespace DistanceMatrix.Repositories
{
    public interface IEnvironmentRepository
    {
        Task Create(long nodeCount);
        Task Delete();
    }
}