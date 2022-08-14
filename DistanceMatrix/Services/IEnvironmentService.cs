using System.Threading.Tasks;

namespace DistanceMatrix.Services
{
    public interface IEnvironmentService
    {
        Task Create(int nodeCount);
        Task Delete();
    }
}