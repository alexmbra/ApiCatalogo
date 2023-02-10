using ApiCatalogo.Context;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProutosPorPreco()
    {
        return Get().OrderBy(c => c.Preco).ToList();
    }
}
