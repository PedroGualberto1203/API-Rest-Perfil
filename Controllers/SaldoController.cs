using ApiPerfil.Data;
using ApiPerfil.Models;
using ApiPerfil.ViewModels;
using ApiPerfil.ViewModels.SaldoViewModel;
using Blog.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPerfil.Controllers
{
    public class SaldoController : ControllerBase
    {
        [HttpGet("v1/saldo")] //Get de todos
        [Authorize]

        public async Task<IActionResult> Get(
        [FromServices] ApiPerfilDataContext context)
        {
            try
            {
                var usuarios = await context
                .Usuarios
                .Select(x => new GetSaldoViewModel
                {
                    Id = x.Id,
                    NomeCompleto = x.NomeCompleto,
                    Saldo = x.Saldo
                })
                .ToListAsync();

                return Ok(new ResultViewModel<List<GetSaldoViewModel>>(usuarios));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<GetSaldoViewModel>>("SAL01 - Falha interna no servidor"));
            }
        }



        [HttpGet("v1/saldo/{id:int}")]  //Get por Id
        [Authorize]

        public async Task<IActionResult> Get(
            [FromServices] ApiPerfilDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var model = await context
                .Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);

                if (model == null)
                    return NotFound(new ResultViewModel<Usuario>("SAL02 - Conteúdo não encontrado"));

                var usuario = new GetSaldoViewModel
                {
                    Id = model.Id,
                    NomeCompleto = model.NomeCompleto,
                    Saldo = model.Saldo
                };

                return Ok(new ResultViewModel<GetSaldoViewModel>(usuario));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<GetSaldoViewModel>("SAL03 - Falha interna no servidor"));
            }
        }



        [HttpPut("v1/saldo/add/{id:int}")] //Adicionar saldo ao user
        [Authorize]

        public async Task<IActionResult> Put(
            [FromServices] ApiPerfilDataContext context,
            [FromRoute] int id,
            [FromBody] CreateSaldoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<List<CreateSaldoViewModel>>(ModelState.GetErros()));

                var usuario = await context
                .Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);

                usuario.Saldo += model.Saldo;

                context.Usuarios.Update(usuario);
                context.SaveChangesAsync();

                var saldo = new CreateSaldoViewModel
                {
                    Saldo = usuario.Saldo
                };

                return Ok(new ResultViewModel<CreateSaldoViewModel>(saldo));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<CreateSaldoViewModel>("SAL04 - Falha interna no servidor"));
            }
        }
    }

    
}