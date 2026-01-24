using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity;

partial class Program {
  static void Main(string[] args) {
    var builder = WebApplication.CreateBuilder(args);
    var appSetting = new AppSetting(builder.Configuration);

    builder.Services.AddDbContext<IdentityContext>(options => options.UseNpgsql(appSetting.Identity));
    builder.Services.AddIdentityCore<UserEntity>(options => {
      options.User.RequireUniqueEmail = true;
      options.Password.RequiredLength = 8;
      options.Password.RequireDigit = true;
      options.Password.RequireLowercase = true;
      options.Password.RequireNonAlphanumeric = true;
      options.Password.RequireUppercase = true;
    })
    .AddEntityFrameworkStores<IdentityContext>();

    // Add authentication/authorization
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    builder.Services.AddAuthorization();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    if (app.Environment.IsDevelopment()) {
      app.UseSwagger();
      app.UseSwaggerUI();
    }
    // Important: order is auth then authorization
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
  }
}
