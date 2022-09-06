using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DistanceMatrix.Entities;
using DistanceMatrix.Objects;
using DistanceMatrix.Objects.Requests;
using Neo4j.Driver;
using Neo4jClient;

namespace DistanceMatrix.Repositories
{
    public class EnvironmentRepository : IEnvironmentRepository
    {
        /*private readonly IDriver _driver;*/
        private readonly IGraphClient _client;

        public EnvironmentRepository( /*IDriver driver, */ IGraphClient client)
        {
            _client = client;
        }

        /*public async Task Create(long nodeCount)
        {
            await Transaction(async t =>
            {
                for (int i = 1; i <= nodeCount; i++)
                {
                    await t.RunAsync($"CREATE (Location:location{{code: \"M1.{i}\"}})");
                    Console.WriteLine($"Current i: {i}");
                }
            });
        }*/

        public async Task CreateNodes(long nodeCount)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var module = 1;
            var corridor = 1;
            for (int i = 1; i <= nodeCount; i++)
            {
                await _client.Cypher
                    .Create($"(:Location {{areaCode: \"1A\", corridor: \"{corridor}\", module: {module}}})")
                    .ExecuteWithoutResultsAsync();

                if (module % 1000 == 0)
                {
                    module = 0;
                    corridor++;
                }

                module++;

                Console.WriteLine($"Current i: {i}");
            }

            Console.WriteLine($"{nodeCount} node created. Elapsed time: {sw.Elapsed.TotalSeconds}");
            sw.Stop();
        }

        public async Task CreateRelations(CreateRelationRequest request)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var relationCount = 3;
            for (int i = 1; i <= relationCount; i++)
            {
                for (int j = i; j <= relationCount; j++)
                {
                    await _client.Cypher.Match($"(l)")
                        .Where((Location l) => l.AreaCode == "1A" && l.Corridor == "1" &&
                                               l.Module == i)
                        .Match("(m)")
                        .Where((Location m) => m.AreaCode == "1A" && m.Corridor == "1" &&
                                               m.Module == j)
                        .Merge($"(l)-[:Distance{{value:1000}}]-(m)")
                        .ExecuteWithoutResultsAsync();
                }

                Console.WriteLine($"Relation count: {i}");
            }
            
            Console.WriteLine($"{relationCount} relation created. Elapsed time: {sw.Elapsed.TotalSeconds}");
            sw.Stop();
        }

        public async Task Delete()
        {
            await _client.Cypher.Match("()-[r]-()")
                .Delete("r")
                .ExecuteWithoutResultsAsync();

            await _client.Cypher.Match("(n)")
                .Delete("(n)")
                .ExecuteWithoutResultsAsync();
        }

        public async Task<Distance> GetRelation(GetRelationRequest request)
        {
            var result = await _client.Cypher.Match($"(fromLocation:Location)-[r:Distance]->(toLocation:Location)")
                .Where((Location fromLocation, Location toLocation) => fromLocation.AreaCode == request.FromAreaCode
                                                                       && fromLocation.Corridor == request.FromCorridor
                                                                       && fromLocation.Module == request.FromModule
                                                                       && toLocation.AreaCode == request.ToAreaCode
                                                                       && toLocation.Corridor == request.ToCorridor
                                                                       && toLocation.Module == request.ToModule
                )
                .Return((fromLocation, toLocation, r) => new Distance
                {
                    Value = r.As<Distance>().Value
                })
                .ResultsAsync;

            return result.FirstOrDefault();
        }
        
        
        /*private static void WithDatabase(SessionConfigBuilder sessionConfigBuilder)
        {
            var neo4JVersion = System.Environment.GetEnvironmentVariable("NEO4J_VERSION") ?? "";
            if (!neo4JVersion.StartsWith("4"))
            {
                return;
            }

            sessionConfigBuilder.WithDatabase(Database());
        }

        private static string Database()
        {
            return System.Environment.GetEnvironmentVariable("NEO4J_DATABASE") ?? "movies";
        }
        
        private async Task Transaction(Func<IAsyncTransaction, Task> func)
        {
            var session = _driver.AsyncSession(WithDatabase);
            var transaction = await session.BeginTransactionAsync();
            try
            {
                await func(transaction);
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        
        public async Task<T> Transaction<T>(Func<IAsyncTransaction, Task<T>> func)
        {
            var session = _driver.AsyncSession(WithDatabase);
            var transaction = await session.BeginTransactionAsync();
            try
            {
                var result = await func(transaction);
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }*/
    }
}