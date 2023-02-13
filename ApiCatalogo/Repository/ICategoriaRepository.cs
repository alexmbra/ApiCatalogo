using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<IEnumerable<Categoria>> GetCategoriascProdutos();

    Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters);
}
