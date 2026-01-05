# 🌐 Overview

The `IEntity` interface defines the basic structure for entities in the system. It ensures that all entities have a unique identifier and track update metadata.

# 🔑 Key Properties

- **Id**: Represents the unique identifier for the entity.
- **UpdatedById**: Tracks the user who last modified the entity.
- **UpdatedDateTime**: Stores the timestamp of the last update.

# 🛠 Usage

Implement this interface in your domain models to ensure consistency across entities:

```csharp
public class Product : IEntity
{
    public Guid Id { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTime UpdatedDateTime { get; set; }

    // Additional properties specific to Product
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

# 🎯 Purpose

This interface promotes:

- **Consistency**: All entities share common metadata.
- **Auditability**: Easy tracking of updates and changes.
- **Extensibility**: Can be extended for additional auditing or versioning features.
