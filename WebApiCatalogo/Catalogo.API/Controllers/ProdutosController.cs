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

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }


        // api/produtos/primeiro
        [HttpGet("/primeiro")]
        public ActionResult<ProdutoModel> GetPrimeiro()
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault();
                if (produto is null)
                {
                    return NotFound("Produtos não encontrados.");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }

        }

        // api/produtos/id
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoModel> Get(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound($"Produto com o id = {id} não encontrado.");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }

        }

        // api/produtos
        [HttpPost]
        public ActionResult Post(ProdutoModel produto)
        {
            try
            {
                if (produto is null)
                {
                    return BadRequest("Não encontrado.");
                }

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }

        }

        // api/produtos/id
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoModel produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest($"Produto id = {id} é diferente.");
                }
                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }

        }

        // api/produtos id
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                {
                    return NotFound($"Produto id = {id} não localizado.");
                }
                _context.Produtos.Remove(produto);
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }

        }
    }
}
