using WebApiCatalogo.Catalogo.Core.Model;

namespace WebApiCatalogo.Catalogo.Infrastucture.Repository
{
    public interface ICategoriaRepository
    {
        IEnumerable<CategoriaModel>GetCategorias();
        CategoriaModel GetCategoriaPorId(int id);
        CategoriaModel GetCategoriaCriar(CategoriaModel categoria);
        CategoriaModel GetCategoriaUpdate(CategoriaModel categoria);
        CategoriaModel GetCategoriaDelete(int id);
    }
}
