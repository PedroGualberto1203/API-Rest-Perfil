using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiPerfil.Models;
using Blog.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace ApiPerfil.Services
{
    public class TokenService // essa class vai servir para gerar o TOKEN em cima de um User
    {
        public string GenerateToken(Usuario user)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); //Manipulador de token(vai gerar o token)
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey); // Pegou a key informada na class Configuration e passou ela em bites, por meio desse encoding... pois ele pede que seja em bites
        var claims = user.GetClaims(); //Pega os claims do usuário, que são as informações que serão passadas no token, como o id, email, etc.
        var tokenDescriptor = new SecurityTokenDescriptor //Contém as informações/configs do token
        {
            Subject = new ClaimsIdentity(claims), //Aqui é onde colocamos os claims que foram pegos do usuário, que são as informações que serão passadas no token
            Expires = DateTime.UtcNow.AddHours(8), //Define o tempo de duração deste Token
            SigningCredentials = new SigningCredentials( //Como este Token vai ser gerado(encriptado) e lido(desencriptado)
                new SymmetricSecurityKey(key), //Chave simétrica, que é a mesma chave usada para encriptar e desencriptar o token
                SecurityAlgorithms.HmacSha256Signature // Aqui foi passado o tipo de algoritmo que vai ser usado para encriptar a chave
            ) 
        }; 
        var token = tokenHandler.CreateToken(tokenDescriptor); //Criar o token
        return tokenHandler.WriteToken(token); //Para retornar como string
    }
    }
}