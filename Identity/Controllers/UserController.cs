using Zuhid.Base;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Mappers;
using Zuhid.Identity.Models;
using Zuhid.Identity.Repositories;

namespace Zuhid.Identity.Controllers;

public class UserController(UserRepository repository, UserMapper mapper) :
  BaseCrudController<UserRepository, UserMapper, IdentityContext, UserModel, UserEntity>(repository, mapper) {
}
