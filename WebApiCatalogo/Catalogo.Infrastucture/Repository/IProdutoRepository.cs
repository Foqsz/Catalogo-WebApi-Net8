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

        IEnumerable<ProdutoModel> GetProdutosPorCategoria(int id);
    }
}
