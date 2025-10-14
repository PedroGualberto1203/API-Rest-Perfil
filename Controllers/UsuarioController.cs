using ApiPerfil.Data;
using ApiPerfil.Models;
using ApiPerfil.Models.UsuarioViewModels;
using ApiPerfil.ViewModels;
using ApiPerfil.ViewModels.PermissaoViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPerfil.Controllers
{
    public class UsuarioController : ControllerBase
    {
        [HttpGet("v1/usuarios/get")] //Get de todos os usuarios
        [Authorize]
        public async Task<IActionResult> Get(
            [FromServices] ApiPerfilDataContext context)
        {
            try
            {
                var usuarios = await context
                    .Usuarios
                    .Include(x => x.Permissoes)
                    .Select(x => new CreateUsuarioViewModel
                    {
                        Id = x.Id,
                        NomeCompleto = x.NomeCompleto,
                        Email = x.Email,
                        Telefone = x.Telefone,
                        Permissoes = x.Permissoes.Select(x => new PermissaoViewModel { Nome = x.Nome }).ToList()
                    })
                    .ToListAsync();

                return Ok(new ResultViewModel<IList<CreateUsuarioViewModel>>(usuarios));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Usuario>>("USU01 - Falha interna no servidor"));
            }
        }



        [HttpGet("v1/usuario/getbyid/{id:int}")] //Get por ID
        [Authorize]
        public async Task<IActionResult> GetById(
            [FromServices] ApiPerfilDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var model = await context
                .Usuarios
                .Include(x => x.Permissoes)
                .FirstOrDefaultAsync(x => x.Id == id);

                if (model == null)
                    return NotFound(new ResultViewModel<Usuario>("USU02 - Conteúdo não encontrado"));

                var usuario = new CreateUsuarioViewModel
                {
                    Id = model.Id,
                    NomeCompleto = model.NomeCompleto,
                    Email = model.Email,
                    Telefone = model.Telefone,
                    Permissoes = model.Permissoes.Select(x => new PermissaoViewModel { Nome = x.Nome }).ToList()
                };

                return Ok(new ResultViewModel<CreateUsuarioViewModel>(usuario));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<CreateUsuarioViewModel>>("USU03 - Falha interna no servidor"));
            }
        }



        [HttpPut("v1/usuario/editar/{id:int}")]//Editar os dados de um user(menos senha e saldo)
        [Authorize]

        public async Task<IActionResult> Edit(
            [FromServices] ApiPerfilDataContext context,
            [FromRoute] int id,
            [FromBody] EditorUsuarioViewModel model)
        {
            try
            {
                var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
                if (model == null)
                    return NotFound(new ResultViewModel<Usuario>("USU04 - Conteúdo não encontrado"));

                if (!string.IsNullOrEmpty(model.NomeCompleto))
                    usuario.NomeCompleto = model.NomeCompleto;

                if (!string.IsNullOrEmpty(model.Telefone))
                    usuario.Telefone = model.Telefone;

                if (!string.IsNullOrEmpty(model.Email))
                    usuario.Email = model.Email;

                if (!string.IsNullOrEmpty(model.CPF))
                    usuario.CPF = model.CPF;

                context.Usuarios.Update(usuario);
                context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    user = usuario.NomeCompleto,
                    usuario.Telefone,
                    usuario.Email,
                    usuario.CPF
                }));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(400, new ResultViewModel<Usuario>("USU05 - Não foi possível criar a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Usuario>>("USU06 - Falha interna no servidor"));
            }
        }



        [HttpDelete("v1/usuario/delete/{id:int}")]//Delete de usuario
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(
            [FromServices] ApiPerfilDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                context.Usuarios.Remove(usuario);
                context.SaveChangesAsync();

                return Ok(new ResultViewModel<Usuario>($"usuário com ID={id} deletado com sucesso!"));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(400, new ResultViewModel<Usuario>("USU07 - Não foi possível criar a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Usuario>("USU08 - Falha interna no servidor"));
            }
        }
        
    }
}