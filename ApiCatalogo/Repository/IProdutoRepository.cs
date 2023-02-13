using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetProdutosPorPreco();

    Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);

}
