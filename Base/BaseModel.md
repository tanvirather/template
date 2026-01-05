# Overview

The `BaseModel` class inherits from `BaseEntity` and provides a standard structure for models in the system. It ensures that all models have a unique identifier and track update metadata through the inherited properties.

# 🛠 Usage

Use `BaseModel` as a base class for your domain models to ensure consistency across entities:

```csharp
public class Product : BaseModel
{
    // Additional properties specific to Product
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

---

# 🎯 Purpose

This class promotes:

- ✅ **Consistency**: All models share common metadata.
- 🔍 **Auditability**: Easy tracking of updates and changes.
- ➕ **Extensibility**: Can be extended for additional auditing or versioning features.
