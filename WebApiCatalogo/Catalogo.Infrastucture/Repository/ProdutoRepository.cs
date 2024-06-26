﻿using Microsoft.EntityFrameworkCore;
using WebApiCatalogo.Catalogo.API.Pagination;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;
using X.PagedList;

namespace WebApiCatalogo.Catalogo.Infrastucture.Repository
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //public IEnumerable<ProdutoModel> GetProdutos(ProdutosParameters produtosParams)
        //{
        //    return GetAll()
        //        .OrderBy(p => p.Nome)
        //        .Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize)
        //        .Take(produtosParams.PageSize).ToList();
        //}

        public async Task <IPagedList<ProdutoModel>> GetProdutosAsync(ProdutosParameters produtosParams)
        {
            var produtos = await GetAllAsync();

            var produtosOrdenados = produtos.OrderBy(p  => p.ProdutoId).AsQueryable();

            var resultado = await produtosOrdenados.ToPagedListAsync(produtosParams.PageNumber, produtosParams.PageSize);

            return resultado;
        }

        public async Task<IPagedList<ProdutoModel>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams)
        {
            var produtos = await GetAllAsync();

            if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
            {
                if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
            }                                                            //erro cs1503 resolvido adicionando AsQueryable()
            var produtosFiltrados = await produtos.ToPagedListAsync(produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

            return produtosFiltrados;
        }

        public async Task<IEnumerable<ProdutoModel>> GetProdutosPorCategoriaAsync(int id)
        {
            var produtos = await GetAllAsync();
            var produtosCategoria = produtos.Where(p => p.CategoriaId == id);
            return produtosCategoria;
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
