using System.Reflection;
using Zuhid.Base;

namespace Zuhid.Product.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplicationExtension.AddServices(args);
        Assembly.GetAssembly(typeof(ProductContext))!.GetTypes().Where(s =>
            s.Name.EndsWith("Repository")
            || s.Name.EndsWith("Mapper")
            || s.Name.EndsWith("Validator")
          )
          .ToList()
          .ForEach(item => builder.Services.AddScoped(item));
        var appSetting = new AppSetting(builder.Configuration);
        builder.Services.AddSingleton(appSetting);

        builder.AddDatabase<ProductContext, ProductContext>(appSetting.ConnectionStrings.Product);
        var app = builder.BuildServices();
        app.Run();
    }
}
