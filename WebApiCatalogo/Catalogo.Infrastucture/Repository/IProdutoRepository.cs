using WebApiCatalogo.Catalogo.API.Pagination;
using WebApiCatalogo.Catalogo.Core.Model;

namespace WebApiCatalogo.Catalogo.Infrastucture.Repository
{
    public interface IProdutoRepository : IRepository<ProdutoModel>
    {
        //IQueryable<ProdutoModel> GetProdutos();
        //ProdutoModel GetProdutoPorId(int id);
        //ProdutoModel GetProdutoCriar(ProdutoModel produto);
        //bool GetProdutoUpdate(ProdutoModel produto);
        //bool GetProdutoDeletar(int id);

        //IEnumerable<ProdutoModel> GetProdutos(ProdutosParameters produtosParams);
        Task<PagedList<ProdutoModel>> GetProdutosAsync(ProdutosParameters produtosParams);
        Task<PagedList<ProdutoModel>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams);
        Task <IEnumerable<ProdutoModel>> GetProdutosPorCategoriaAsync(int id);


    }
}
