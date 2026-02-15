using Zuhid.Weather.Etls;

namespace Zuhid.Weather.Jobs;

public class Program {
  static async Task Main(string[] args) {
    await new TafEtl().Run();
  }
}
