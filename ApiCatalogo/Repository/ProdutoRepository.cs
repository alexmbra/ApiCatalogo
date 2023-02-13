using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
    {
        return await Get().OrderBy(c => c.Preco).ToListAsync();
    }


    public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
    {
        return await PagedList<Produto>.ToPagedList(
                Get().OrderBy(p => p.ProdutoId), 
                produtosParameters.PageNumber, 
                produtosParameters.PageSize);
    }
}
