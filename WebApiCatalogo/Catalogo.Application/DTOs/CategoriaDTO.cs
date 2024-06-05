using System.ComponentModel.DataAnnotations;

namespace WebApiCatalogo.Catalogo.Application.DTOs
{
    public class CategoriaDTO
    {

        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(40, ErrorMessage = "O Nome deve ter entre 5 e 20 caracteres.", MinimumLength = 5)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
    }
}
