using Microsoft.EntityFrameworkCore;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;

namespace WebApiCatalogo.Catalogo.Infrastucture.Repository
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ProdutoModel> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        } 

        //Listar Produtos
        //public IQueryable<ProdutoModel> GetProdutos()
        //{
        //    var produtos = _context.Produtos;
        //    return produtos;
        //}

        //public ProdutoModel GetProdutoPorId(int id)
        //{
        //    var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id); //FirstOrDefault vai diretamente no banco de dados
        //    if (produto == null)
        //    {
        //        throw new InvalidOperationException("Produto é null");
        //    }
        //    return produto;
        //}

        //public ProdutoModel GetProdutoCriar(ProdutoModel produto)
        //{
        //    if (produto == null)
        //    {
        //        throw new InvalidOperationException("Produto é null");
        //    }
        //    _context.Produtos.Add(produto);
        //    _context.SaveChanges();
        //    return produto;
        //}

        //public bool GetProdutoUpdate(ProdutoModel produto)
        //{
        //    if (produto == null)
        //    {
        //        throw new ArgumentNullException(nameof(produto));
        //    }

        //    if (_context.Produtos.Any(p => p.ProdutoId == produto.ProdutoId)) // se existir um produto na coleção de objetos produto cujo o ProdutoId é igual o Id do produto que estou recebendo
        //    {
        //        _context.Produtos.Update(produto);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        //public bool GetProdutoDeletar(int id)
        //{
        //    var produto = _context.Produtos.Find(id); //Find vai diretamente na memória, mas só pode ser usado se for uma chave primaria

        //    if (produto == null)
        //    {
        //        throw new ArgumentNullException(nameof(produto));
        //    }

        //    if (produto is not null)
        //    {
        //        _context.Produtos.Remove(produto);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false; 
        //}
    }
}
