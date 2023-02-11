using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository;

public interface ICategoriaRepository : IRepository<Categoria>
{
    IEnumerable<Categoria> GetCategoriascProdutos();

    PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
}
