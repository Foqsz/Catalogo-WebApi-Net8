using System.ComponentModel.DataAnnotations;

namespace WebApiCatalogo.Catalogo.Core.Model;

public class ProdutoModel
{
    [Key]
    public int ProdutoId { get; set; }

    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastroo { get; set; }
    public int CategoriaId { get; set; }
    public CategoriaModel? Categoria { get; set; }
}


