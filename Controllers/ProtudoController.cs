using ApiPerfil.Data;
using ApiPerfil.Models;
using ApiPerfil.ViewModels;
using ApiPerfil.ViewModels.CategoriaViewModels;
using ApiPerfil.ViewModels.ProdutoViewModels;
using Blog.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPerfil.Controllers
{
    public class ProdutoController : ControllerBase
    {
        [HttpGet("v1/produtos")]
        [Authorize]

        public async Task<IActionResult> Get(// Get de todos os produtos
            [FromServices] ApiPerfilDataContext context)
        {
            try
            {
                var produtos = await context
                    .Produtos
                    .Include(x => x.Categoria)
                    .Select(x => new GetProdutoViewModel
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Quantidade = x.Quantidade,
                        Preco = x.Preco,
                        Categoria = new CategoriaNomeViewModel
                        {
                            Nome = x.Categoria.Nome
                        }
                    })
                    .ToListAsync();

                return Ok(new ResultViewModel<List<GetProdutoViewModel>>(produtos));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<GetProdutoViewModel>>("PROD01 - Falha interna no servidor"));
            }
        }




        [HttpGet("v1/produto/{id:int}")]// Get de UM produto pelo ID
        [Authorize]

        public async Task<IActionResult> GetById(
            [FromServices] ApiPerfilDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var model = await context
                .Produtos
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id == id);

                if (model == null)
                    return NotFound(new ResultViewModel<Produto>("PROD02 - Conteúdo não encontrado"));

                var produto = new GetProdutoViewModel
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Quantidade = model.Quantidade,
                    Preco = model.Preco,
                    Categoria = new CategoriaNomeViewModel
                    {
                        Nome = model.Categoria.Nome
                    }
                };

                return Ok(new ResultViewModel<GetProdutoViewModel>(produto));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<GetProdutoViewModel>("PROD03 - Falha interna no servidor"));
            }
        }




        [HttpPost("v1/produto/create")] // Create de um produto
        [Authorize]

        public async Task<IActionResult> Post(
            [FromServices] ApiPerfilDataContext context,
            [FromBody] CreateProdutoViewModel model)
        {
            if (!ModelState.IsValid)// ModelState.IsValid é uma propriedade que verifica se o modelo recebido é válido de acordo com as regras de validação definidas no tipo do modelo. Esse tipo de erro ocorre quando o usuario envia um modelo que não obedece as regras de validação
                return BadRequest(new ResultViewModel<List<CreateProdutoViewModel>>(ModelState.GetErros())); // Vai retornar a lista de erros, erros estes definidos no modelo EditorCategoryViewModel, por exemplo, se o nome for vazio ou passar do numero máximo de caracteres aceito. GetErros foi o método criado por nos para fazer a lista de erros das props

            try
            {
                var produto = new Produto
                {
                    Nome = model.Nome,
                    Quantidade = model.Quantidade,
                    Preco = model.Preco,
                    CategoriaID = model.CategoriaID
                };

                await context.Produtos.AddAsync(produto);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<CreateProdutoViewModel>("PROD04 - Falha interna no servidor"));
            }
        }




        [HttpPut("v1/produto/edit/{id:int}")] // Editar dados de um produto
        [Authorize]

        public async Task<IActionResult> Edit(
            [FromServices] ApiPerfilDataContext context,
            [FromRoute] int id,
            [FromBody] EditProdutoViewModel model)
        { 

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<List<EditProdutoViewModel>>(ModelState.GetErros())); // Vai pegar o erro v de model vazio verificado no Attribute "VerificacaoModelVazio"

            try
            {
                var produto = await context
                .Produtos
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id == id);

                if (!string.IsNullOrEmpty(model.Nome)) //Verificação se o nome do model nao é null
                    produto.Nome = model.Nome;

                if (model.Quantidade.HasValue) //Verificação se a quantidade do model nao é null
                    produto.Quantidade = model.Quantidade.Value;

                if (model.Preco.HasValue) //Verificação se o preço do model nao é null
                    produto.Preco = model.Preco.Value;

                if (model.CategoriaID.HasValue) //Verificação se a categoria do model nao é null
                    produto.CategoriaID = model.CategoriaID.Value;

                context.Produtos.Update(produto);
                context.SaveChangesAsync();

                var prod = new EditProdutoViewModel
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Quantidade = produto.Quantidade,
                    Preco = produto.Preco,
                    Categoria = new CategoriaNomeViewModel
                    {
                        Nome = produto.Categoria.Nome
                    }
                };

                return Ok(new ResultViewModel<EditProdutoViewModel>(prod));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Usuario>>("PROD05 - Falha interna no servidor"));
            }
        }




        [HttpDelete("v1/produto/delete/{id:int}")] //Delete
        [Authorize]

        public async Task<IActionResult> Delete(
            [FromServices] ApiPerfilDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

                context.Produtos.Remove(produto);
                context.SaveChangesAsync();

                return Ok(new ResultViewModel<Produto>($"Produto com ID={id} deletado com sucesso!"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Produto>("PROD06 - Falha interna no servidor"));
            }
        }
    }
}