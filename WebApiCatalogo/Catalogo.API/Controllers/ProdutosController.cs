using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;
using WebApiCatalogo.Catalogo.Infrastucture.Repository;

namespace WebApiCatalogo.Catalogo.API.Controllers
{
    [Route("[controller]")] // /produtos
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        private readonly ILogger _logger;

        public ProdutosController(IProdutoRepository repository, ILogger<ProdutosController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
         
        [HttpGet("/Produtos")]
        public ActionResult<IEnumerable<ProdutoModel>> GetProdutos()
        {
            var produto = _repository.GetProdutos().ToList();
            if (produto is null)
            {
                _logger.LogWarning("Produtos não encontrados.");
                return NotFound("Produtos não encontrados.");
            }
            return Ok(produto);
        }
         
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoModel> Get(int id)
        {

            var produto = _repository.GetProdutoPorId(id);
            if (produto is null)
            {
                _logger.LogWarning($"O produto com o id {id} não foi encontrado.");
                return NotFound($"Produto com o id = {id} não encontrado.");
            }
            return produto;
        }
         
        [HttpPost]
        public ActionResult Post(ProdutoModel produto)
        {

            if (produto is null)
            {
                _logger.LogWarning("Produto não encontrado.");
                return BadRequest("Não encontrado.");
            }
            _repository.GetProdutoCriar(produto);
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto); 
        }
          
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoModel produto)
        {
            if (id != produto.ProdutoId)
            {
                _logger.LogWarning($"ProdutoId: {id} deu erro. Id diferente.");
                return BadRequest($"Produto id = {id} é diferente.");
            }

            bool atualizado = _repository.GetProdutoUpdate(produto);

            if (atualizado)
            {
                return Ok(produto);
            }
            else
            {
                return StatusCode(500, $"Falha ao atualizar o produto de id = {id}.");
            }
             
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            bool produto = _repository.GetProdutoDeletar(id);

            if (produto == null)
            {
                _logger.LogWarning($"O produto id {id} não foi localizado.");
                return NotFound($"Produto id = {id} não localizado.");
            }

            if (produto)
            {
                return Ok($"Produto de id = {id} foi deletado.");
            }
            else
            {
                return StatusCode(500, $"Falha ao deletar o produto de id = {id}.");
            } 
        }
    }
}
