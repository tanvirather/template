using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Zuhid.Base;

public static class WebApplicationExtension {
  public static WebApplicationBuilder AddServices(string[] args) {
    var builder = WebApplication.CreateBuilder(args);
    // if (builder.Environment.IsDevelopment())
    // {
    builder.Services.AddSwaggerGen();
    // }
    builder.Services.AddControllers();
    builder.Services.AddCors(options => options.AddPolicy(name: "CorsPolicy", policy => policy
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()
    ));

    // Add BaseMapper to service collection, so you don't have to create a mapper for each entity
    // builder.Services.AddScoped(typeof(BaseMapper<,>));

    // This will add all classes that end with Repository, Mapper, and Validator to the service collection
    Assembly.GetCallingAssembly().GetTypes().Where(s =>
        s.IsClass && (
          s.Name.EndsWith("Repository")
          || s.Name.EndsWith("Mapper")
          || s.Name.EndsWith("Validator")
        )
      )
      .ToList()
      .ForEach(item => builder.Services.AddScoped(item));
    return builder;
  }

  public static WebApplication BuildServices(this WebApplicationBuilder builder) {
    var app = builder.Build();
    app.UseCors("CorsPolicy");
    // if (app.Environment.IsDevelopment())
    // {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
    });
    // }
    app.MapGet("/", async context => await context.Response.WriteAsync("""
    <html>
    <body style='padding:100px 0;text-align:center;font-size:xxx-large;'>
      <a href='/swagger/index.html'>View Swagger</a>
    </body>
    </html>
    """));
    app.MapControllers();
    return app;
  }

  public static void AddDatabase<ITContext, TContext>(this WebApplicationBuilder builder, string connectionString)
    where ITContext : class
    where TContext : DbContext, ITContext {
    builder.Services.AddDbContext<TContext>(options => options
      .UseNpgsql(connectionString)
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // setting to no tracking to improve performance
    );
  }
}
