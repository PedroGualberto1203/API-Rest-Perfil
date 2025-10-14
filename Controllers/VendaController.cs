// Não esqueça os usings necessários no topo do arquivo
using System.Security.Claims;
using ApiPerfil.Data;
using ApiPerfil.Models;
using ApiPerfil.ViewModels;
using ApiPerfil.ViewModels.VendaViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
public class VendaController : ControllerBase
{
    [HttpPost("v1/venda/create")]
    [Authorize]
    public async Task<IActionResult> Post(
        [FromServices] ApiPerfilDataContext context,
        [FromBody] CarrinhoVendaViewModel model)
    {

        //Valida se o model do carrinho está ou nao vazio
        if (model?.Itens == null || !model.Itens.Any())
            return BadRequest(new ResultViewModel<CarrinhoVendaViewModel>("Carrinho não pode estar vazio"));

        //Pega o ID do usuário logado
        var idUsuarioLogado = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
        if (idUsuarioLogado == 0)
            return Unauthorized(new ResultViewModel<CarrinhoVendaViewModel>("Usuário não autenticado"));

        //O erros é para no final avaliar, se tiver pelo menos um erro, toda a operação é cancelada
        var erros = new List<string>();
        decimal valorTotalVenda = 0;

        //Guardou os IDs do produtos
        var idsProdutosNoCarrinho = model.Itens.Select(i => i.ProdutoId).ToList();

        //Com o Contains, comparou os itens na varievel com os do banco, os que baterem, armazena na variavel
        var produtosDoBanco = await context
        .Produtos
        .AsNoTracking()
        .Where(x => idsProdutosNoCarrinho.Contains(x.Id))
        .ToDictionaryAsync(x => x.Id);

        //Busca o usuário que está fazendo a compra
        var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == idUsuarioLogado);
        if (usuario == null)
            return NotFound(new ResultViewModel<CarrinhoVendaViewModel>("Usuário não encontrado."));

        //Loop para validar cada item do carrinho
        foreach (var itemCarrinho in model.Itens)
        {
            // O produto solicitado no model nao foi encontrado no banco?
            if (!produtosDoBanco.TryGetValue(itemCarrinho.ProdutoId, out var produtoDoBanco))
            {
                erros.Add($"O produto com ID {itemCarrinho.ProdutoId} não existe.");
                continue;
            }

            //A quantidade em estoque é suficiente?
            if (produtoDoBanco.Quantidade < itemCarrinho.Quantidade)
                erros.Add($"Estoque insuficiente para o produto com ID {produtoDoBanco.Nome}. Restam {produtoDoBanco.Quantidade} unidades.");

            valorTotalVenda += itemCarrinho.Quantidade * produtoDoBanco.Preco;
        }

        // O saldo o usuário é suficiente para a compra?
        if (usuario.Saldo < valorTotalVenda)
            erros.Add($"Saldo insuficiente, seu Saldo atual: {usuario.Saldo}, Valor da compra: {valorTotalVenda}.");

        // Se houver qualquer erro de validação, retorna todos eles de uma vez
        if (erros.Any())
            return BadRequest(new ResultViewModel<List<string>>(erros));

        using (var transacao = await context.Database.BeginTransactionAsync())
        {
            try
            {
                //Debita o saldo do usuario
                usuario.Saldo -= valorTotalVenda;
                context.Usuarios.Update(usuario);

                //Cria o registro da venda
                var novaVenda = new Venda
                {
                    UsuarioID = idUsuarioLogado,
                    ValorTotal = valorTotalVenda,
                    DataVenda = DateTime.UtcNow,
                    VendaItems = new List<VendaItem>()
                };

                var vendaReturn = new ReturnVendaViewModel
                {
                    VendaId = novaVenda.VendaID,
                    UsuarioID = novaVenda.UsuarioID,
                    DataVenda = novaVenda.DataVenda,
                    ValorTotal = novaVenda.ValorTotal,
                    VendaItems = new List<VendaItemViewModel>()
                };

                //Debita o estoque dos produtos e cria os VendaItens
                foreach (var itemCarrinho in model.Itens)
                {
                    var produtoDoBanco = await context.Produtos.FindAsync(itemCarrinho.ProdutoId);
                    if (produtoDoBanco != null)
                    {
                        produtoDoBanco.Quantidade -= itemCarrinho.Quantidade;
                        context.Produtos.Update(produtoDoBanco);

                        novaVenda.VendaItems.Add(new VendaItem
                        {
                            ProdutoID = itemCarrinho.ProdutoId,
                            Quantidade = itemCarrinho.Quantidade,
                            PrecoUnitario = produtoDoBanco.Preco
                        });

                        vendaReturn.VendaItems.Add(new VendaItemViewModel
                        {
                            ProdutoID = itemCarrinho.ProdutoId,
                            Quantidade = itemCarrinho.Quantidade,
                            PrecoUnitario = produtoDoBanco.Preco
                        });

                    }
                }

                await context.Vendas.AddAsync(novaVenda);

                await context.SaveChangesAsync();

                await transacao.CommitAsync();

                return Ok(new ResultViewModel<ReturnVendaViewModel>(vendaReturn, null));
            }
            catch (DbUpdateException ex)
            {
                await transacao.RollbackAsync();
                return StatusCode(400, new ResultViewModel<CarrinhoItemViewModel>("USU05 - Não foi possível realizar a venda"));
            }
            catch
            {
                await transacao.RollbackAsync();
                return StatusCode(500, new ResultViewModel<string>("Falha interna ao processar a venda"));
            }
        }
    }

    [HttpGet("v1/vendas/get")] //Get de todas as vendas
    [Authorize]
    public async Task<IActionResult> Get(
        [FromServices] ApiPerfilDataContext context)
    {
        try
        {
            var vendas = await context
        .Vendas
        .Include(x => x.VendaItems)
        .Select(x => new GetVendaViewModel
        {
            VendaId = x.VendaID,
            UsuarioID = x.UsuarioID,
            DataVenda = x.DataVenda,
            ValorTotal = x.ValorTotal,
            VendaItems = x.VendaItems.Select(x => new BaseGetVendaViewModel
            {
                VendaItemID = x.VendaItemID,
                ProdutoID = x.ProdutoID,
                Quantidade = x.Quantidade,
                PrecoUnitario = x.PrecoUnitario
            }).ToList()
        })
        .ToListAsync();

            return Ok(new ResultViewModel<List<GetVendaViewModel>>(vendas));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<ReturnVendaViewModel>>("Falha interna no servidor"));
        }
    }


    [HttpGet("v1/venda/getbyid/{valorTotal:decimal}")] //Get por ID
    [Authorize]

    public async Task<IActionResult> GetByValor(
    [FromServices] ApiPerfilDataContext context,
    [FromRoute] decimal valorTotal)
{
    var model = await context
        .Vendas
        .Include(x => x.VendaItems)
        .FirstOrDefaultAsync(x => x.ValorTotal == valorTotal);

    if (model == null)
        return NotFound(new ResultViewModel<string>("Conteúdo não encontrado"));

    var venda = new GetVendaViewModel
    {
        UsuarioID = model.UsuarioID,
        VendaId = model.VendaID,
        DataVenda = model.DataVenda,
        ValorTotal = model.ValorTotal,
        VendaItems = model.VendaItems.Select(item => new BaseGetVendaViewModel
        {
            VendaItemID = item.VendaItemID,
            ProdutoID = item.ProdutoID,
            Quantidade = item.Quantidade,
            PrecoUnitario = item.PrecoUnitario
        }).ToList()
    };

    return Ok(new ResultViewModel<GetVendaViewModel>(venda));
}

}