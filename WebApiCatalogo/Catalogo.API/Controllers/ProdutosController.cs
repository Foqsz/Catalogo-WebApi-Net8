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
            var produto = _repository.GetAll().ToList();
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

            var produto = _repository.Get(p => p.ProdutoId == id);
            if (produto is null)
            {
                _logger.LogWarning($"O produto com o id {id} não foi encontrado.");
                return NotFound($"Produto com o id = {id} não encontrado.");
            }
            return Ok(produto);
        }

        [HttpGet("/Produtos/Produtos/{id}")]
        public ActionResult<ProdutoModel> GetId(int id)
        {

            var produto = _repository.GetProdutosPorCategoria(id);
            if (produto is null)
            {
                _logger.LogWarning($"O produto com o id {id} não foi encontrado.");
                return NotFound($"Produto com o id = {id} não encontrado.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(ProdutoModel produto)
        {

            if (produto is null)
            {
                _logger.LogWarning("Produto não encontrado.");
                return BadRequest("Não encontrado.");
            }
            _repository.Create(produto);
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

            var atualizado = _repository.Update(produto);
            return Ok(produto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var produto = _repository.Get(p => p.ProdutoId == id);

            if (produto == null)
            {
                _logger.LogWarning($"O produto id {id} não foi localizado.");
                return NotFound($"Produto id = {id} não localizado.");
            }
            var produtoDeletado = _repository.Delete(produto); 
            return Ok(produto);

        }
    }
}
