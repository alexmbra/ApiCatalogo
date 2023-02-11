using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosPorPreco()
    {
        return Get().OrderBy(c => c.Preco).ToList();
    }

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        //return Get()
        //    .OrderBy(on => on.Nome)
        //    .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
        //    .Take(produtosParameters.PageSize)
        //    .ToList();

        return PagedList<Produto>.ToPagedList(
                Get().OrderBy(p => p.ProdutoId), 
                produtosParameters.PageNumber, 
                produtosParameters.PageSize);
    }
}
