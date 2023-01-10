using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mB)
        {
            mB.Sql("Insert into Categorias(Nome, ImagemUrl) Values('Bebidas', 'bebidas.jpg')");
            mB.Sql("Insert into Categorias(Nome, ImagemUrl) Values('lanches', 'lanches.jpg')");
            mB.Sql("Insert into Categorias(Nome, ImagemUrl) Values('Sobremesas', 'Sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder mB)
        {
            mB.Sql("delete from Categorias");
        }
    }
}
