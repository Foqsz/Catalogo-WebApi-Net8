using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Produtos (Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastroo,CategoriaId) " +
            "Values('Coca-Cola Diet', 'Refrigerante de Cola 350 ml',5.45,'cocacola.jpg',50,now(),1)");

            migrationBuilder.Sql("Insert into Produtos (Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastroo,CategoriaId) " +
            "Values('Pipos Vitaminado', 'Pipos Vitaminado pequeno',2.30,'pips.jpg',10,now(),2)");

            migrationBuilder.Sql("Insert into Produtos (Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastroo,CategoriaId) " +
            "Values('Açai', 'Açai de Copo Grande',9.50,'acai.jpg',20,now(),3)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Produtos");
        }
    }
}
