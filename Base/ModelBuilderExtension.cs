using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace Zuhid.Base;

public static class ModelBuilderExtension {

  public static ModelBuilder ToSnakeCase(this ModelBuilder builder, string schema = "public") {
    foreach (var entity in builder.Model.GetEntityTypes()) {
      entity.SetSchema(entity.GetSchema() ?? schema); // Set schema if provided 
      entity.SetTableName(entity.GetTableName()!.ToSnakeCase()); // Convert table name to snake_case
      entity.GetProperties().ToList().ForEach(property => property.SetColumnName(property.Name!.ToSnakeCase())); // Convert column names to snake_case
      entity.GetKeys().ToList().ForEach(key => key.SetName(key.GetName()?.ToLower())); // Convert key names to lower case
      entity.GetForeignKeys().ToList().ForEach(fk => fk.SetConstraintName(fk.GetConstraintName()?.ToLower())); // Convert foreign key names to lower case
      entity.GetIndexes().ToList().ForEach(index => index.SetDatabaseName(index.GetDatabaseName()?.ToLower())); // Convert index names to lower case
    }
    return builder;
  }

  public static void LoadJsonData<TEntity>(this ModelBuilder builder) where TEntity : class {
    var data = JsonSerializer.Deserialize<List<TEntity>>(File.ReadAllText($"Dataload/{typeof(TEntity).Name}.json"));
    if (data != null && data.Count > 0) {
      builder.Entity<TEntity>().HasData(data);
    }
  }

  public static void LoadCsvData<TEntity>(this ModelBuilder builder) where TEntity : class, new() {
    // Console.WriteLine($"Loading CSV data for {typeof(TEntity).Name}...");
    var lines = File.ReadAllLines($"Dataload/{typeof(TEntity).Name}.csv");
    if (lines.Length == 0) {
      return;
    }
    // Read header row
    var headers = lines[0].Split(',');
    // Process each line (skipping header)
    foreach (var line in lines.Skip(1)) {
      if (string.IsNullOrWhiteSpace(line)) {
        continue;
      }
      var values = line.Split(',');
      var entity = new TEntity();
      // Console.WriteLine($"Processing line: {line}");
      for (var i = 0; i < headers.Length; i++) {
        var prop = typeof(TEntity).GetProperty(headers[i], BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        var value = ConvertValue(values[i], prop!.PropertyType);
        prop.SetValue(entity, value);
      }
      builder.Entity<TEntity>().HasData(entity);
    }
  }

  private static object? ConvertValue(string rawValue, Type targetType) {
    return string.IsNullOrWhiteSpace(rawValue)
      ? null
      : targetType.FullName switch {
        "System.String" => rawValue,
        "System.Guid" => Guid.Parse(rawValue),
        "System.Int32" => int.Parse(rawValue, CultureInfo.InvariantCulture),
        "System.Int64" => long.Parse(rawValue, CultureInfo.InvariantCulture),
        "System.Boolean" => bool.Parse(rawValue),
        "System.DateTime" => DateTime.Parse(rawValue, CultureInfo.InvariantCulture),
        _ => null,
      };
  }
}
