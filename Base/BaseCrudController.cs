using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Zuhid.Base;

[ApiController]
[Route("[controller]")]
public class BaseCrudController<TRepository, TMapper, TContext, TModel, TEntity>(TRepository repository, TMapper mapper) : ControllerBase
    where TRepository : BaseRepository<TContext, TModel, TEntity>
    where TMapper : BaseMapper<TModel, TEntity>
    where TContext : DbContext
    where TModel : class, IEntity, new()
    where TEntity : class, IEntity, new() {

  [HttpGet()]
  public virtual async Task<List<TModel>> Get(Guid id = default) {
    return await repository.Get(id).ConfigureAwait(false);
  }

  [HttpPost]
  public virtual async Task Add([FromBody] TModel model) {
    await repository.Add(mapper.GetEntity(model)).ConfigureAwait(false);
  }

  [HttpPut()]
  public virtual async Task Update([FromBody] TModel model) {
    await repository.Update(mapper.GetEntity(model)).ConfigureAwait(false);
  }

  [HttpDelete()]
  public virtual async Task Delete(Guid id) {
    await repository.Delete(id).ConfigureAwait(false);
  }
}
