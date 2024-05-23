using System.Collections.ObjectModel;

namespace WebApiCatalogo.Catalogo.Core.Model;

public class CategoriaModel
{
    public CategoriaModel()
    {
        Produtos = new Collection<ProdutoModel>();
    }

    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set;}

    public ICollection<ProdutoModel>? Produtos { get; set; }
}

