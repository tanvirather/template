using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Zuhid.Base;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Services;

namespace Zuhid.Identity;

partial class Program {
  static void Main(string[] args) {
    var builder = WebApplicationExtension.AddServices(args);
    var appSetting = new AppSetting(builder.Configuration);
    builder.AddDatabase<IdentityContext, IdentityContext>(appSetting.Identity);
    builder.Services.AddIdentityCore<UserEntity>(options => {
      options.User.RequireUniqueEmail = true;
      options.Password.RequiredLength = 8;
      options.Password.RequireDigit = true;
      options.Password.RequireLowercase = true;
      options.Password.RequireNonAlphanumeric = true;
      options.Password.RequireUppercase = true;
    })
      // .AddRoles<IdentityRole>()
      .AddSignInManager()
      .AddEntityFrameworkStores<IdentityContext>()
      .AddDefaultTokenProviders();
    builder.Services.AddTransient<ITokenService>(_ => new JwtTokenService(appSetting.IdentityModel));
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    builder.Services.AddAuthorization();
    builder.BuildServices().Run();
  }
}
