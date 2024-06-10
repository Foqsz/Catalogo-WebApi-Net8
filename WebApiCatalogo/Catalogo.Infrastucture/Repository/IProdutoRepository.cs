using WebApiCatalogo.Catalogo.API.Pagination;
using WebApiCatalogo.Catalogo.Core.Model;
using X.PagedList;

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
        Task<IPagedList<ProdutoModel>> GetProdutosAsync(ProdutosParameters produtosParams);
        Task<IPagedList<ProdutoModel>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams);
        Task <IEnumerable<ProdutoModel>> GetProdutosPorCategoriaAsync(int id);


    }
}
