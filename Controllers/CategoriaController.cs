using ApiPerfil.Data;
using ApiPerfil.Models;
using ApiPerfil.Models.CategoriaViewModels;
using ApiPerfil.ViewModels;
using Blog.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPerfil.Controllers
{
    [ApiController]
    [Route("")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet("v1/categorias")] //Get de todas as categorias
        [Authorize]
        public async Task<IActionResult> Get(
            [FromServices] ApiPerfilDataContext context)
        {
            try
            {
                var categorias = await context.Categorias.ToListAsync();
                return Ok(new ResultViewModel<List<Categoria>>(categorias));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Categoria>>("CA01 - Falha interna no servidor"));
            }
        }


        [HttpGet("v1/categoria/{id:int}")] //Get de uma categoria(por ID)
        [Authorize]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            [FromServices] ApiPerfilDataContext context)
        {
            try
            {
                var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
                if (categoria == null)
                    return NotFound(new ResultViewModel<Categoria>("CA02 - Conteúdo não encontrado"));

                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Categoria>>("CA03 - Falha interna no servidor"));
            }
        }


        [HttpPost("v1/categoria/create")] //Create de uma categoria
        [Authorize]
        public async Task<IActionResult> Post(
            [FromBody] EditorCategoriaViewModel model,
            [FromServices] ApiPerfilDataContext context)
        {

            if (!ModelState.IsValid)// ModelState.IsValid é uma propriedade que verifica se o modelo recebido é válido de acordo com as regras de validação definidas no tipo do modelo. Esse tipo de erro ocorre quando o usuario envia um modelo que não obedece as regras de validação
                return BadRequest(new ResultViewModel<Categoria>(ModelState.GetErros())); // Vai retornar a lista de erros, erros estes definidos no modelo EditorCategoryViewModel, por exemplo, se o nome for vazio ou passar do numero máximo de caracteres aceito. GetErros foi o método criado por nos para fazer a lista de erros das props

            try
            {
                var categoria = new Categoria
                {
                    Nome = model.Nome
                };

                await context.Categorias.AddAsync(categoria);
                await context.SaveChangesAsync();

                return Created($"v1/categoria/create/{categoria.Id}", new ResultViewModel<Categoria>(categoria));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>("CA04 - Não foi possível incluir a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Categoria>("CA05 - Erro interno do servidor"));
            }
        }


        [HttpPut("v1/categoria/edit/{id:int}")] //Edit de uma categoria
        [Authorize]
        public async Task<IActionResult> Put(
            [FromRoute] int id,
            [FromBody] EditorCategoriaViewModel model,
            [FromServices] ApiPerfilDataContext context)
        {
            try
            {
                var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

                if (categoria == null)
                    return NotFound(new ResultViewModel<Categoria>("CA06 - Conteúdo não encontrado"));

                categoria.Nome = model.Nome;

                context.Categorias.Update(categoria);
                await context.SaveChangesAsync();

                return Created($"v1/categoria/{categoria.Id}", new ResultViewModel<Categoria>(categoria));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>("CA07 - Não foi possível modificar a categoria"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>("CA08 - Erro interno do servidor"));
            }
        }


        [HttpDelete("v1/categoria/delete/{id:int}")] //Delete de uma categoria
        [Authorize]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            [FromServices] ApiPerfilDataContext context)
        {
            try
            {
                var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

                if (categoria == null)
                    return NotFound(new ResultViewModel<Categoria>("CA09 - Conteúdo não encontrado"));

                context.Categorias.Remove(categoria);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>("CA10 - Não foi possível excluir a categoria"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>("CA11 - Erro interno do servidor"));
            }
        }
    }
}