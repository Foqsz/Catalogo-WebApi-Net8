using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiCatalogo.Catalogo.Core.Model;

[Table("Categorias")]
public class CategoriaModel
{
    public CategoriaModel()
    {
        Produtos = new Collection<ProdutoModel>();
    }

    [Key]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(40, ErrorMessage = "O Nome deve ter entre 5 e 20 caracteres.", MinimumLength = 5)] 
    public string Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string ImagemUrl { get; set;}

    [JsonIgnore]
    public ICollection<ProdutoModel>? Produtos { get; set; } 
}

