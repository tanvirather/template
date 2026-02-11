using Zuhid.Base;
using Zuhid.Product.Hubs;

namespace Zuhid.Product;

public class Program {
  public static void Main(string[] args) {
    var builder = WebApplicationExtension.AddServices(args);
    var appSetting = new AppSetting(builder.Configuration);
    builder.AddDatabase<ProductContext, ProductContext>(appSetting.Product);
    builder.Services.AddSignalR();
    var app = builder.BuildServices();
    // app.MapGet("/api/health", () => new { status = "ok", time = DateTimeOffset.UtcNow });
    app.MapHub<ChatHub>("/hubs/chat" /*, options => { options.Transports = ... }*/);
    app.Run();
  }
}
