using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Categoria> GetCategoriascProdutos()
    {
        return Get().Include(x => x.Produtos);
    }

    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
    {
        return PagedList<Categoria>.ToPagedList(
                Get().OrderBy(c => c.CategoriaId),
                categoriasParameters.PageNumber,
                categoriasParameters.PageSize);
    }
}
