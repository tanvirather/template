using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5103/hubs/chat", options =>
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
await connection.InvokeAsync("SendMessage", "Tanvir", "Hello from .NET client");
// Console.ReadLine();
