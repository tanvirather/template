using Microsoft.AspNetCore.Components;
using Zuhid.Base;
using Zuhid.Product.Entities;
using Zuhid.Product.Models;
using Zuhid.Product.Repositories;

namespace Zuhid.Product.Controllers;

[Route("[controller]")]
public class NumericTypeController(NumericTypeRepository repository, BaseMapper<NumericTypeEntity, NumericTypeModel> mapper)
  : BaseCrudController<NumericTypeRepository, BaseMapper<NumericTypeEntity, NumericTypeModel>, ProductContext, NumericTypeEntity, NumericTypeModel>(repository, mapper)
{
}
