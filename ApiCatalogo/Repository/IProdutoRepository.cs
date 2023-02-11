using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorPreco();

    PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);

}
