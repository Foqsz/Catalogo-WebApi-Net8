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

        Task <PagedList<CategoriaModel>> GetCategoriasAsync(CategoriasParameters categoriasParams);
        Task <PagedList<CategoriaModel>> GetProdutoNomeAsync(CategoriasFiltroNome categoriasFiltroParams);    
    }
}
