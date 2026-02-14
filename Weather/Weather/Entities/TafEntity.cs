
using System.ComponentModel.DataAnnotations;

namespace Zuhid.Weather.Entities;

public class TafEntity {
  // public int Id { get; set; }
  [Key]
  public string IcaoId { get; set; } = string.Empty;
  public string? Name { get; set; }
  // public List<TafForecastEntity> Fcsts { get; set; } = [];
}

// public sealed class TafForecastEntity {
//   public long? TimeFrom { get; set; }
//   public long? TimeTo { get; set; }
//   public int? Wdir { get; set; }
//   public int? Wspd { get; set; }
//   public int? Wgst { get; set; }
// }

