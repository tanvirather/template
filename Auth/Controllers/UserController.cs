using Zuhid.Auth.Entities;
using Zuhid.Auth.Mappers;
using Zuhid.Auth.Models;
using Zuhid.Auth.Repositories;
using Zuhid.Base;

namespace Zuhid.Auth.Controllers;

public class UserController(UserRepository repository, UserMapper mapper) :
  BaseController<UserRepository, UserMapper, AuthDbContext, UserModel, UserEntity>(repository, mapper) {
}
