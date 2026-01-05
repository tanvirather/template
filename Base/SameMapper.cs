namespace Zuhid.Base;

public class SameMapper<TSource> : BaseMapper<TSource, TSource> where TSource : new() {
  /// <summary>Maps a single source to the same type.</summary>
  public override TSource Map(TSource source) => source;

  /// <summary>Maps a list of sources to the same type.</summary>
  public override List<TSource> MapList(List<TSource> modelList) => modelList;
}

