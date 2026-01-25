using Zuhid.Auth.Entities;
using Zuhid.Auth.Models;
using Zuhid.Auth.Tools;
using Zuhid.Base;

namespace Zuhid.Auth.Mappers;

public class AccountMapper : BaseMapper<AccountModel, UserEntity> {
  public override UserEntity GetEntity(AccountModel model) {
    var entity = base.GetEntity(model);
    entity.HashedPassword = SecurityTool.HashPassword(model.Password);
    return entity;
  }
}
