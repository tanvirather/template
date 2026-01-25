namespace Zuhid.Base;

public class SameMapper<TSource> : BaseMapper<TSource, TSource> where TSource : new() {

  public override TSource GetEntity(TSource model) => model;

  public override TSource GetModel(TSource entity) => entity;

  public new List<TSource> GetEntityList(List<TSource> modelList) => modelList;
  public new List<TSource> GetModelList(List<TSource> entityList) => entityList;
}
