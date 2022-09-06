using System.Collections.Generic;
using System.Threading.Tasks;
using DistanceMatrix.Entities;
using DistanceMatrix.Objects;
using DistanceMatrix.Objects.Requests;

namespace DistanceMatrix.Services
{
    public interface IEnvironmentService
    {
        Task CreateNodes(int nodeCount);
        Task CreateRelations(CreateRelationRequest request);
        Task Delete();
        Task<Distance> GetRelation(GetRelationRequest request);
    }
}