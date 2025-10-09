using System.Security.Claims;
using ApiPerfil.Models;

namespace Blog.Extensions;

public static class RoleClaimsExtensions
{
    public static IEnumerable<Claim> GetClaims(this Usuario user)
    {
        var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email), //O Claim Name vai ser o email do user
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        result.AddRange(
            user.Permissoes.Select(role => new Claim(ClaimTypes.Role, role.Nome)) //O Claim Role vai ser role.Nome
        );
        return result;
    }
}