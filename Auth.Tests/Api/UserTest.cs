using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Zuhid.Auth.Models;

namespace Zuhid.Auth.Tests.Api;

public class UserTest : BaseApiTest {
  [Fact]
  public async Task CrudTest() {

    // Arrange
    var addModel = new UserModel {
      Id = Guid.NewGuid(),
      Email = "test_1@email.com",
    };
    var updateModel = new UserModel {
      Id = addModel.Id,
      Email = "test_2@email.com",
    };

    // Get: Act and Assert
    var response = await Client.GetAsync($"/User");
    var responseList = await response.Content.ReadFromJsonAsync<List<UserModel>>();
    Assert.True(response.IsSuccessStatusCode);

    // Add: Act and Assert
    response = await Client.PostAsync("/User", new StringContent(JsonSerializer.Serialize(addModel), Encoding.UTF8, "application/json"));
    Assert.True(response.IsSuccessStatusCode);
    response = await Client.GetAsync($"/User?Id={addModel.Id}");
    responseList = await response.Content.ReadFromJsonAsync<List<UserModel>>();
    Assert.Equal(1, responseList?.Count);
    Assert.Equal(addModel.Id, responseList?[0].Id);
    Assert.Equal(addModel.Email, responseList?[0].Email);

    // Update: Act and Assert
    response = await Client.PutAsync("/User", new StringContent(JsonSerializer.Serialize(updateModel), Encoding.UTF8, "application/json"));
    Assert.True(response.IsSuccessStatusCode);
    response = await Client.GetAsync($"/User?Id={addModel.Id}");
    responseList = await response.Content.ReadFromJsonAsync<List<UserModel>>();
    Assert.Equal(1, responseList?.Count);
    Assert.Equal(updateModel.Id, responseList?[0].Id);
    Assert.Equal(updateModel.Email, responseList?[0].Email);

    // Delete: Act and Assert 
    response = await Client.DeleteAsync($"/User?Id={addModel.Id}");
    Assert.True(response.IsSuccessStatusCode);
    response = await Client.GetAsync($"/User?Id={addModel.Id}");
    responseList = await response.Content.ReadFromJsonAsync<List<UserModel>>();
    Assert.Equal(0, responseList?.Count);
  }
}
