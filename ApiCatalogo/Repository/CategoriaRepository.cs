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

    public async Task<IEnumerable<Categoria>> GetCategoriascProdutos()
    {
        return await Get().Include(x => x.Produtos).ToListAsync();
    }

    public async Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters)
    {
        return  await PagedList<Categoria>.ToPagedList(
                Get().OrderBy(c => c.CategoriaId),
                categoriasParameters.PageNumber,
                categoriasParameters.PageSize);
    }


}
