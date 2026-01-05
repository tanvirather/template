# Overview

`BaseMapper<TSource, TDestination>` is a simple reflection-based mapper that copies property values from a source object of type `TSource` to a new instance of `TDestination` when the two types share property names. `TDestination` must have a public parameterless constructor.

# Usage

Add `using Zuhid.Base;` and instantiate the mapper (or subclass it) to map between two types that share property names:

```csharp
using Zuhid.Base;

var mapper = new BaseMapper<MyModel, MyEntity>();
var entity = mapper.Map(model);
var entities = mapper.MapList(models);
```

# API Reference

## `TDestination Map(TSource model)`

- Description: Creates a new `TDestination` instance and copies values for all public properties where the property name matches between `TSource` and `TDestination`.
- Parameters: `model` — the source object to map from.
- Returns: A new `TDestination` with matched properties set from `model`.
- Notes: Uses `Type.GetProperties()` and `PropertyInfo.SetValue` / `GetValue` via reflection. Only properties with identical names are copied; property type mismatches may produce runtime errors when `SetValue` is called.

## `List<TDestination> MapList(List<TSource> modelList)`

- Description: Maps a list of `TSource` objects to a list of `TDestination` instances by calling `Map` for each element.
- Parameters: `modelList` — list of source objects.
- Returns: A `List<TDestination>` where each item is the mapped result of the corresponding item in `modelList`.
- Notes: Uses `List<TSource>.ForEach` to iterate; preserves input order.

## Edge Cases & Recommendations

- Null handling: The current implementation does not guard against `model` or `modelList` being `null`. Calling `Map(null)` will throw a `NullReferenceException` when attempting to access properties on the `model` parameter. Consider adding null checks before mapping.
- Empty lists: `MapList` returns an empty `List<TDestination>` when given an empty `modelList`.
- Property type mismatches: If properties share a name but have incompatible types, `PropertyInfo.SetValue` may throw an exception at runtime. Prefer mapping between compatible types or add conversion logic in a derived mapper.
- Performance: Reflection is used for property discovery and value transfer; for high-throughput scenarios consider caching `PropertyInfo` arrays or using compiled expressions/IL emit for faster mapping.
- Inheritance & accessibility: Only public instance properties are considered by `GetProperties()`. Private or protected members are ignored.

## Example: Adding a safe null-guard wrapper

```csharp
public class SafeMapper<TSource, TDestination> : BaseMapper<TSource, TDestination>
  where TDestination : new()
{
  public override TDestination Map(TSource model)
  {
    if (model == null) throw new ArgumentNullException(nameof(model));
    return base.Map(model);
  }

  public override List<TDestination> MapList(List<TSource> modelList)
  {
    if (modelList == null) throw new ArgumentNullException(nameof(modelList));
    return base.MapList(modelList);
  }
}
```
