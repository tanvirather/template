using Microsoft.AspNetCore.Identity;
using Zuhid.Base;

namespace Zuhid.Identity.Entities;

public class RoleEntity : IdentityRole<Guid>, IEntity {
  public Guid UpdatedById { get; set; }
  public DateTime UpdatedDateTime { get; set; }
}

