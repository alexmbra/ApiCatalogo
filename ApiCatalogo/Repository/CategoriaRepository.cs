using ApiCatalogo.Context;
using ApiCatalogo.Models;
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

}
