using Zuhid.Base;
using Zuhid.Product.Entities;
using Zuhid.Product.Models;

namespace Zuhid.Product.Repositories;

public class NumericTypeRepository(ProductContext context) : BaseRepository<ProductContext, NumericTypeEntity, NumericTypeModel>(context)
{
    protected override IQueryable<NumericTypeModel> Query => _context.NumericType.Select(entity => new NumericTypeModel
    {
        Id = entity.Id,
        UpdatedById = entity.UpdatedById,
        Updated = entity.Updated,
        SmallintType = entity.SmallintType,
        IntegerType = entity.IntegerType,
        BigintType = entity.BigintType,
    });
}
