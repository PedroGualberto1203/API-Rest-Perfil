using ApiPerfil.Data;
using ApiPerfil.Models;
using ApiPerfil.Services;
using ApiPerfil.ViewModels;
using ApiPerfil.ViewModels.UsuarioViewModels;
using Blog.Extensions;
using Blog.ViewModels.UsuarioViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace ApiPerfil.Controllers
{
    public class AccountController : ControllerBase
    {
        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
        [FromBody] LoginViewModel model,
        [FromServices] ApiPerfilDataContext context,
        [FromServices] TokenService tokenService) //Vamos gerar um token e enviar para tela
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErros())); //Se o modelo nao estiver certo, retorna a nossa lista de erro

            var user = await context
                .Usuarios
                .Include(x => x.Permissoes)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválido"));

            if (!PasswordHasher.Verify(user.SenhaHash, model.Password)) // Se a senha salva no banco nao for igual a do model...Com esse Verify ele consegue fazer a comparação entre a senha encriptada do banco com a digitada pelo user(não encriptada)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválido"));

            try
            {
                var token = tokenService.GenerateToken(user); // Se a senha for a mesma, gera o token pra esse user
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("LG01 - Falha interna no sistema"));
            }
        }



        [HttpPost("v1/accounts/create")]  //REGISTRO DE USUÁRIO
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAccount(
            [FromBody] RegisterViewModel model,
            [FromServices] EmailService emailService,
            [FromServices] ApiPerfilDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErros())); //Se o modelo nao estiver certo, retorna a nossa lista de erro

            var password = PasswordGenerator.Generate(25); //Gera uma senha de 25 caracteres(pacote do balta(//Pacote para geração de senha do balta: dotnet add package SecureIdentity))
            model.SenhaHash = PasswordHasher.Hash(password); //Faz o encriptamento da senha

            var permissao = context.Permissoes.FirstOrDefault(x => x.Id == 2); //Seleciona a permissao "UsuarioPadrao"
            model.Permissoes.Add(permissao);

            var user = new Usuario
            {
                NomeCompleto = model.NomeCompleto,
                Email = model.Email,
                Telefone = model.Telefone,
                CPF = model.CPF,
                SenhaHash = model.SenhaHash,
                Permissoes = model.Permissoes
            };

            var msg = "Senha enviada por Email! Confira sua caixa de entrada/spam.";

            try
            {
                await context.Usuarios.AddAsync(user);
                await context.SaveChangesAsync();

                emailService.Send(
                    user.NomeCompleto,
                    user.Email,
                    subject: "Bem-vindo a Perfil Acessórios",
                    body: $"Sua senha é: {password}"
                );

                return Ok(new ResultViewModel<dynamic>(new //dynamic para que n tenha que criar um Model específico para esse tipo de retorno, é como se fosse um tipo neutro kk
                {
                    user = user.Email, // Retorna o email do usuário
                    msg
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("RE01 - Este E-mail já existe"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("RE02 - Falha interna no servidor"));
            }
        }
    }
}