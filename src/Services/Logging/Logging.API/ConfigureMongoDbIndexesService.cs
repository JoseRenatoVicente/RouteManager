using Logging.Domain.Entities.v1;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Logging.API;

public class ConfigureMongoDbIndexesService : IHostedService
{
    private readonly IMongoDatabase _database;

    public ConfigureMongoDbIndexesService(IMongoDatabase database)
        => _database = database;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<Log>(nameof(Log));

        var indexKeysDefinition = Builders<Log>.IndexKeys.Ascending(log => log.EntityId);
        await collection.Indexes.CreateOneAsync(new CreateIndexModel<Log>(indexKeysDefinition), cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}