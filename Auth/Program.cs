using Zuhid.Base;

namespace Zuhid.Auth;

public class Program {
  public static void Main(string[] args) {
    var builder = WebApplicationExtension.AddServices(args);
    var appSetting = new AppSetting(builder.Configuration);
    builder.AddDatabase<AuthDbContext, AuthDbContext>(appSetting.Auth);
    builder.BuildServices().Run();
  }
}
