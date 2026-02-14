using Zuhid.Base;

namespace Zuhid.Weather;

public class Program {
  public static void Main(string[] args) {
    var builder = WebApplicationExtension.AddServices(args);
    var appSetting = new AppSetting(builder.Configuration);
    builder.AddDatabase<WeatherContext, WeatherContext>(appSetting.Product);
    builder.Services.AddSingleton(sp => new NewClass().GetContainer());
    builder.BuildServices().Run();
  }
}
