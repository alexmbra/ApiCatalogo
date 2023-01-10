using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    public partial class PopulateProdutos : Migration
    {
        protected override void Up(MigrationBuilder mB)
        {
            mB.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Coca-cola Diet','Refrigerante de Cola 350 ml', 5.45, 'cocacola.jpg', 50, now(), 1)");

            mB.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Lanche de atum','Lanche de atum com maionese', 8.50, 'atum.jpg', 10, now(), 2)");

            mB.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Pudim 100g','Pudim de leite condensado 100g', 6.75, 'pudim.jpg', 20, now(), 3)");
        }

        protected override void Down(MigrationBuilder mB)
        {
            mB.Sql("delete from Produtos");
        }
    }
}
