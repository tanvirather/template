using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Zuhid.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class ForecastController(Container container) : ControllerBase {

  [HttpGet("{id}")]
  public async Task<ActionResult<MyItem>> Get(string id) {
    var response = await container.ReadItemAsync<MyItem>(
      id: id,
      partitionKey: new PartitionKey(id)
    );
    response.Resource.ETag = response.ETag;
    return Ok(response.Resource);
  }

  [HttpPost]
  public async Task<ActionResult<MyItem>> Add([FromBody] MyItem input) {
    if (string.IsNullOrWhiteSpace(input.id)) {
      input.id = Guid.NewGuid().ToString("n");
    }
    try {
      var response = await container.CreateItemAsync(item: input, partitionKey: new PartitionKey(input.id));
      // Surface ETag back to client
      var created = response.Resource;
      created.ETag = response.ETag;
      return CreatedAtAction(nameof(Get), new { created.id }, created);
    } catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict) {
      // Item with same id already exists
      return Conflict(new { message = $"Item with id '{input.id}' already exists." });
    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(string id, [FromBody] MyItem input, CancellationToken ct) {
    if (!string.Equals(id, input.id, StringComparison.Ordinal)) {
      return BadRequest(new { message = "Route id must match body id." });
    }
    var options = new ItemRequestOptions();

    // If-Match handling for optimistic concurrency
    if (Request.Headers.TryGetValue("If-Match", out var ifMatchValues)) {
      var etag = ifMatchValues.FirstOrDefault();
      if (!string.IsNullOrWhiteSpace(etag)) {
        options.IfMatchEtag = etag;
      }
    }

    try {
      var response = await container.ReplaceItemAsync(
          id: id,
          partitionKey: new PartitionKey(id),
          item: input,
          requestOptions: options,
          cancellationToken: ct);

      var updated = response.Resource;
      updated.ETag = response.ETag;

      return Ok(updated);
    } catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.PreconditionFailed) {
      // ETag mismatch (concurrent update)
      return StatusCode((int)HttpStatusCode.PreconditionFailed,
          new { message = "ETag mismatch. The item has been modified by another process." });
    } catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound) {
      return NotFound();
    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(string id, CancellationToken ct) {
    try {
      await container.DeleteItemAsync<MyItem>(id: id, partitionKey: new PartitionKey(id), cancellationToken: ct);
      return NoContent();
    } catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound) {
      return NotFound();
    }
  }
}
