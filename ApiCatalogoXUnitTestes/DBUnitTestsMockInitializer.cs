using ApiCatalogo.Context;
using ApiCatalogo.Models;

namespace ApiCatalogoXUnitTestes;

public class DBUnitTestsMockInitializer
{
	public DBUnitTestsMockInitializer()
	{

	}

	public void Seed(AppDbContext context)
	{
        context.Categorias.Add
        (new Categoria { Nome = "Bebidas999", ImagemUrl = "bebidas999.jpg" });

        context.Categorias.Add
        (new Categoria { Nome = "Sucos", ImagemUrl = "sucos1.jpg" });

        context.Categorias.Add
        (new Categoria { Nome = "Doces", ImagemUrl = "doces1.jpg" });

        context.Categorias.Add
        (new Categoria { Nome = "Salgados", ImagemUrl = "Salgados1.jpg" });

        context.Categorias.Add
        (new Categoria { Nome = "Tortas", ImagemUrl = "tortas1.jpg" });

        context.Categorias.Add
        (new Categoria { Nome = "Bolos", ImagemUrl = "bolos1.jpg" });

        context.Categorias.Add
        (new Categoria { Nome = "Lanches", ImagemUrl = "lanches1.jpg" });

        context.SaveChanges();

    }
}
