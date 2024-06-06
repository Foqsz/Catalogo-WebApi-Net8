using System.ComponentModel.DataAnnotations;
using WebApiCatalogo.Catalogo.Core.Model;

namespace WebApiCatalogo.Catalogo.Application.DTOs
{
    public class ProdutoDTOUpdateResponse
    { 
        public int ProdutoId { get; set; } 
        public string? Nome { get; set; } 
        public string? Descricao { get; set; } 
        public decimal Preco { get; set; } 
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastroo { get; set; }
        public int CategoriaId { get; set; }  
    }
}
