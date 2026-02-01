using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Zuhid.Identity.Services;

public interface ITokenService {
  void Configure(JwtBearerOptions options);
  string Build(Guid id, IList<Claim> claims, IList<string> roles);
}
