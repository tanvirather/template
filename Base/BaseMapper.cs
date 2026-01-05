namespace Zuhid.Base;

public class BaseMapper<TSource, TDestination> where TDestination : new() {
  public virtual TDestination Map(TSource model) {
    var modelProperities = typeof(TSource).GetProperties();
    var entityProperities = typeof(TDestination).GetProperties();
    var entity = new TDestination();
    foreach (var entityProperity in entityProperities) {
      var modelProperity = modelProperities.FirstOrDefault(sourceProperity => entityProperity.Name == sourceProperity.Name);
      if (modelProperity != null) {
        entityProperity.SetValue(entity, modelProperity.GetValue(model, null), null);
      }
    }
    return entity;
  }

  public virtual List<TDestination> MapList(List<TSource> modelList) {
    var entityList = new List<TDestination>();
    modelList.ForEach(model => entityList.Add(Map(model)));
    return entityList;
  }
}
