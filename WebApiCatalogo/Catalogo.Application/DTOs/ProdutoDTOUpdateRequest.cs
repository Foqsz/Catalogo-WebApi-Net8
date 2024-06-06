using System.ComponentModel.DataAnnotations;

namespace WebApiCatalogo.Catalogo.Application.DTOs
{
    public class ProdutoDTOUpdateRequest : IValidatableObject
    {
        [Range(1, 9999, ErrorMessage = "Estoque deve estar entre 1 e 9999")]
        public float Estoque { get; set; }
        public DateTime DataCadastroo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataCadastroo.Date <= DateTime.Now)
            {
                yield return new ValidationResult("A Data deve ser maior que a data atual.",
                    new[] { nameof(this.DataCadastroo) });
            }
        }
    }
}
