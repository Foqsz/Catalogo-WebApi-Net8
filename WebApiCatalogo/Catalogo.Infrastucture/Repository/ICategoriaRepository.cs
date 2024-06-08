using WebApiCatalogo.Catalogo.API.Pagination;
using WebApiCatalogo.Catalogo.Core.Model;

namespace WebApiCatalogo.Catalogo.Infrastucture.Repository
{
    public interface ICategoriaRepository : IRepository<CategoriaModel>
    {
        //IEnumerable<CategoriaModel>GetCategorias();
        //CategoriaModel GetCategoriaPorId(int id);
        //CategoriaModel GetCategoriaCriar(CategoriaModel categoria);
        //CategoriaModel GetCategoriaUpdate(CategoriaModel categoria);
        //CategoriaModel GetCategoriaDelete(int id);

        PagedList<CategoriaModel> GetCategorias(CategoriasParameters categoriasParams);
        PagedList<CategoriaModel> GetProdutoNome(CategoriasFiltroNome categoriasFiltroParams);    
    }
}
