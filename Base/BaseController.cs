using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Zuhid.Base;

[ApiController]
[Route("[controller]")]
public class BaseController<TRepository, TMapper, TContext, TModel, TEntity>(TRepository repository, TMapper mapper) : ControllerBase
    where TRepository : BaseRepository<TContext, TModel, TEntity>
    where TMapper : BaseMapper<TModel, TEntity>
    where TContext : DbContext
    where TModel : IEntity
    where TEntity : class, IEntity, new() {

  [HttpGet()]
  public async Task<List<TModel>> Get(Guid id = default) {
    return await repository.Get(id).ConfigureAwait(false);
  }

  [HttpPost]
  public virtual async Task Add([FromBody] TModel model) {
    await repository.Add(mapper.Map(model)).ConfigureAwait(false);
  }

  [HttpPut()]
  public async Task Update([FromBody] TModel model) {
    await repository.Update(mapper.Map(model)).ConfigureAwait(false);
  }

  [HttpDelete()]
  public async Task Delete(Guid id) {
    await repository.Delete(id).ConfigureAwait(false);
  }
}
