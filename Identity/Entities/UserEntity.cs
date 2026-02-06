using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Zuhid.Base;

namespace Zuhid.Identity.Entities;

public class UserEntity : IdentityUser<Guid>, IEntity {
  public Guid UpdatedById { get; set; }
  [Column(TypeName = "timestamp without time zone")] public DateTime UpdatedDateTime { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
}
