using System;
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

        public async Task Create(long nodeCount)
        {
            await Transaction(async t =>
            {
                await t.RunAsync("CREATE (Location:location{code: \"M1.22\"})");
                await t.RunAsync("CREATE (Location:location{code: \"M1.23\"})");
            });
        }

        public async Task Delete()
        {
            await Transaction(async t =>
            {
                await t.RunAsync("match ()-[r]-() delete (r);");
                await t.RunAsync("match(n) delete (n);");
            });
        }

        private static void WithDatabase(SessionConfigBuilder sessionConfigBuilder)
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
        }
    }
}