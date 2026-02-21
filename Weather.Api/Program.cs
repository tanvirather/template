using System.Reflection;
using Zuhid.Base;

namespace Zuhid.Weather.Api;

public class Program {
  public static void Main(string[] args) {
    var builder = WebApplicationExtension.AddServices(args);
    Assembly.GetAssembly(typeof(PostgresContext))!.GetTypes().Where(s =>
    s.IsClass && (
      s.Name.EndsWith("Repository")
      || s.Name.EndsWith("Mapper")
      || s.Name.EndsWith("Validator")
    )
  )
  .ToList()
  .ForEach(item => builder.Services.AddScoped(item));
    var appSetting = new AppSetting(builder.Configuration);
    builder.AddDatabase<PostgresContext, PostgresContext>(appSetting.Weather);
    builder.Services.AddSingleton(sp => new CosmosContext(new CosmosOptions()));
    var app = builder.BuildServices();
    app.Run();
  }
}
