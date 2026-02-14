using System.Text.Json.Serialization;

namespace Zuhid.Weather.Controllers;

public class MyItem {
  public string id { get; set; }
  public string Name { get; set; }

  // Optional: capture Cosmos ETag for optimistic concurrency
  // This gets populated on reads; include in PUT via If-Match header to enforce concurrency
  [JsonPropertyName("_etag")]
  public string? ETag { get; set; }

}

