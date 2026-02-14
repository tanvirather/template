using Microsoft.Azure.Cosmos;

namespace Zuhid.Weather;

public class NewClass {

  private async Task<Container> MyContainer() {
    // options for local emulator with self-signed certificate
    var options = new CosmosClientOptions() {
      HttpClientFactory = () => new HttpClient(new HttpClientHandler() {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      }),
      ConnectionMode = ConnectionMode.Gateway,
    };

    CosmosClient client = new(
      accountEndpoint: "https://localhost:8081/",
      authKeyOrResourceToken: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
      clientOptions: options
    );

    var databaseResponse = await client.CreateDatabaseIfNotExistsAsync(id: "ProductsDb", throughput: 400).ConfigureAwait(false);
    var container = await databaseResponse.Database.CreateContainerIfNotExistsAsync(id: "ItemsContainer", partitionKeyPath: "/id").ConfigureAwait(false);
    return container;
  }

  public Container GetContainer() {
    return MyContainer().GetAwaiter().GetResult();
  }
}
