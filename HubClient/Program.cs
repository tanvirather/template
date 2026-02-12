using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5103/hubs/table", options =>
    {
      // If using JWT:
      // options.AccessTokenProvider = () => Task.FromResult("<JWT>");
    })
    .WithAutomaticReconnect()
    // .AddMessagePackProtocol() // if server enabled
    .Build();

connection.On<string, string>("ReceiveMessage", (user, message) =>
    Console.WriteLine($"{user}: {message}")
);

await connection.StartAsync();
Console.WriteLine("Connected.");
for (int i = 0; i < 10; i++)
{
  await connection.InvokeAsync("SendMessage", $"Tanvir-{i}", $"Hello from .NET client-{i}");
}
// Console.ReadLine();
