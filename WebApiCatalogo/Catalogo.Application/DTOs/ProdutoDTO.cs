using System.ComponentModel.DataAnnotations;

namespace WebApiCatalogo.Catalogo.Application.DTOs
{
    public class ProdutoDTO
    { 
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
        public int CategoriaId { get; set; }
    }
}
