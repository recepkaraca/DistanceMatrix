using System.Collections.Generic;
using System.Threading.Tasks;
using DistanceMatrix.Entities;
using DistanceMatrix.Objects;
using DistanceMatrix.Objects.Requests;

namespace DistanceMatrix.Repositories
{
    public interface IEnvironmentRepository
    {
        Task CreateNodes(long nodeCount);
        Task CreateRelations(CreateRelationRequest request);
        Task Delete();
        Task<Distance> GetRelation(GetRelationRequest request);
    }
}