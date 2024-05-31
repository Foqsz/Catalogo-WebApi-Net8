using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;

namespace WebApiCatalogo.Catalogo.API.Controllers
{
    [Route("api/[controller]")] // /produtos
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public ProdutosController(AppDbContext context, ILogger<ProdutosController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // api/produtos
        [HttpGet("/produtos")]
        public ActionResult<ProdutoModel> GetPrimeiro()
        {
            var produto = _context.Produtos.ToList();
            if (produto is null)
            {
                _logger.LogWarning("Produtos não encontrados.");
                return NotFound("Produtos não encontrados.");
            }
            return Ok(produto);
        }

        // api/produtos/id
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoModel> Get(int id)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                _logger.LogWarning($"O produto com o id {id} não foi encontrado.");
                return NotFound($"Produto com o id = {id} não encontrado.");
            }
            return produto;
        }

        // api/produtos
        [HttpPost]
        public ActionResult Post(ProdutoModel produto)
        {

            if (produto is null)
            {
                _logger.LogWarning("Produto não encontrado.");
                return BadRequest("Não encontrado.");
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);

        }

        // api/produtos/id
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoModel produto)
        {
            if (id != produto.ProdutoId)
            {
                _logger.LogWarning($"ProdutoId: {id} deu erro. Id diferente.");
                return BadRequest($"Produto id = {id} é diferente.");
            }
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(produto);

        }

        // api/produtos id
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                _logger.LogWarning($"O produto id {id} não foi localizado.");
                return NotFound($"Produto id = {id} não localizado.");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(produto);

        }
    }
}
