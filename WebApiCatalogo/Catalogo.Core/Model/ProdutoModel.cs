using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiCatalogo.Catalogo.Application.Validations;

namespace WebApiCatalogo.Catalogo.Core.Model;

[Table("Produtos")]
public class ProdutoModel : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(40, ErrorMessage = "O Nome deve ter entre 5 e 20 caracteres.", MinimumLength = 5)]
    //[PrimeiraLetraMaiusculaAtributte]
    public string Nome { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "A Descrição deve ter no máximo {30} caracteres.")]
    public string Descricao { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "O Preço deve estar entre {1} e {1000}.")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastroo { get; set; }
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public CategoriaModel? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new //yield indica que o método ou o operador é um interador
                    ValidationResult("A primeira letra do produto deve ser maiúscula.",
                new[]
                {nameof(this.Nome) }
                );
            }
        }
        if (this.Estoque <= 0)
        {
            yield return new //yield indica que o método ou o operador é um interador
                ValidationResult("O estoque deve ser maior que zero",
                new[]
                {nameof(this.Estoque) }
                 );
        }
    }
}



