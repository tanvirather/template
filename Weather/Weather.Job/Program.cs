using Zuhid.Weather.Etls;

namespace Zuhid.Weather.Job;

public class Program
{
  static async Task Main(string[] args)
  {
    await new TafEtl().Run();
  }
}
