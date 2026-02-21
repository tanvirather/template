using Microsoft.Azure.Cosmos;

namespace Zuhid.Weather;

public interface ICosmosContext {
  Container Tafs { get; }
}


public class CosmosContext : ICosmosContext {

  public Container Tafs { get; private set; } = default!;

  public CosmosContext(CosmosOptions cosmosOptions) {
    Initialize(cosmosOptions).GetAwaiter().GetResult(); // blocking call to initialize the context
  }

  private async Task Initialize(CosmosOptions cosmosOptions) {
    var options = new CosmosClientOptions() {
#if DEBUG
      HttpClientFactory = () => new HttpClient(new HttpClientHandler() {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      }),
#endif
      ConnectionMode = ConnectionMode.Gateway,
    };
    var cosmosClient = new CosmosClient(cosmosOptions.Endpoint, cosmosOptions.Key, options);

    // Create DB if not exists
    var dbResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosOptions.DatabaseId).ConfigureAwait(false);
    var database = dbResponse.Database;

    // Users container
    var usersProps = new ContainerProperties(id: "Tafs", partitionKeyPath: "/icaoId") {
      IndexingPolicy = new IndexingPolicy { Automatic = true, IndexingMode = IndexingMode.Lazy }, // indexing policy tuning
      // UniqueKeyPolicy = new UniqueKeyPolicy { UniqueKeys = { new UniqueKey { Paths = { "/tenantId", "/email" } } } }, //unique keys
      DefaultTimeToLive = 60 * 60 * 24 * 1 // 1 day1 TTL for automatic deletion of items, set to -1 for infinite retention
    };
    Tafs = (await database.CreateContainerIfNotExistsAsync(usersProps, ThroughputProperties.CreateManualThroughput(1000)).ConfigureAwait(false)).Container;
  }
}

