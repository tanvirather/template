using Microsoft.EntityFrameworkCore;

namespace Zuhid.Base;

/// <summary>
/// Extension methods for ModelBuilder
/// </summary>
public static class ModelBuilderExtension {

  /// <summary>
  /// Converts all table, column, keys, and indexes names in the model to snake_case.
  /// </summary>
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
}
