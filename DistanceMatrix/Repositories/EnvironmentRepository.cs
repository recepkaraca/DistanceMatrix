using System.Threading.Tasks;
using Neo4j.Driver;

namespace DistanceMatrix.Repositories
{
    public class EnvironmentRepository : IEnvironmentRepository
    {
        private readonly IDriver _driver;

        public EnvironmentRepository(IDriver driver)
        {
            _driver = driver;
        }

        public async Task<int> Create(long nodeCount)
        {
            var session = _driver.AsyncSession(WithDatabase);
            try
            {
                return await session.WriteTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync("CREATE (Location:location{code: \"M1.8\"});");

                    var summary = await cursor.ConsumeAsync();
                    return summary.Counters.PropertiesSet;
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        private static void WithDatabase(SessionConfigBuilder sessionConfigBuilder)
        {
            var neo4jVersion = System.Environment.GetEnvironmentVariable("NEO4J_VERSION") ?? "";
            if (!neo4jVersion.StartsWith("4"))
            {
                return;
            }

            sessionConfigBuilder.WithDatabase(Database());
        }

        private static string Database()
        {
            return System.Environment.GetEnvironmentVariable("NEO4J_DATABASE") ?? "movies";
        }
    }
}