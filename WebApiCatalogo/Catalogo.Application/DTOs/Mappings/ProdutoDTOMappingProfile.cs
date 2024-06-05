using AutoMapper;
using WebApiCatalogo.Catalogo.Core.Model;

namespace WebApiCatalogo.Catalogo.Application.DTOs.Mappings
{
    public class ProdutoDTOMappingProfile : Profile
    {
        public ProdutoDTOMappingProfile()
        {
            //Perfis de mapeamento
            //Mapeamento dos objetos para DTO
            CreateMap<ProdutoModel, ProdutoDTO>().ReverseMap();
            CreateMap<CategoriaModel,  CategoriaDTO>().ReverseMap();
        }
    }
}
