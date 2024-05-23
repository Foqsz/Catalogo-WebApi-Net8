using Microsoft.EntityFrameworkCore;
using WebApiCatalogo.Catalogo.Core.Model;

namespace WebApiCatalogo.Catalogo.Infrastucture.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<CategoriaModel>? Categorias { get; set; }
    public DbSet<ProdutoModel>? Produtos { get; set; }    
}

