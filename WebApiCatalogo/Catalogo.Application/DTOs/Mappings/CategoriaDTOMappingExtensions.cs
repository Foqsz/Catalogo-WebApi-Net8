using WebApiCatalogo.Catalogo.Core.Model;

namespace WebApiCatalogo.Catalogo.Application.DTOs.Mappings
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaDTO? ToCategoriaDTO(this CategoriaModel categoria)
        {
            if (categoria is null)
            {
                return null;
            }

            return new CategoriaDTO()
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl,
            };
        }

        public static CategoriaModel? ToCategoria(this CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
            {
                return null;
            }

            return new CategoriaModel
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl,
            };
        }

        public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<CategoriaModel> categorias) //Recebo uma lista do objeto categorias
        {
            if (categorias is null || !categorias.Any()) //verifico se a lista é nula ou não contem nenhum elemento
            {
                return new List<CategoriaDTO>(); //se ocorrer eu retorno uma lista vazia
            }

            return categorias.Select(categoria => new CategoriaDTO //caso contrario eu projeto cada objeto da lista categorias em um novo objeto da categoria dto
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl,
            }).ToList(); //aqui eu retorno a lista de objeto categoria dto
        }
    }
}

