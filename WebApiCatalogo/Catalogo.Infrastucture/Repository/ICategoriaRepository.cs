using WebApiCatalogo.Catalogo.API.Pagination;
using WebApiCatalogo.Catalogo.Core.Model;
using X.PagedList;

namespace WebApiCatalogo.Catalogo.Infrastucture.Repository
{
    public interface ICategoriaRepository : IRepository<CategoriaModel>
    {
        //IEnumerable<CategoriaModel>GetCategorias();
        //CategoriaModel GetCategoriaPorId(int id);
        //CategoriaModel GetCategoriaCriar(CategoriaModel categoria);
        //CategoriaModel GetCategoriaUpdate(CategoriaModel categoria);
        //CategoriaModel GetCategoriaDelete(int id);

        Task <IPagedList<CategoriaModel>> GetCategoriasAsync(CategoriasParameters categoriasParams);
        Task <IPagedList<CategoriaModel>> GetProdutoNomeAsync(CategoriasFiltroNome categoriasFiltroParams);    
    }
}
