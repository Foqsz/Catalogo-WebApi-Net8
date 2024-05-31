using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCatalogo.Catalogo.Application.Filters;
using WebApiCatalogo.Catalogo.Application.Interface;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;

namespace WebApiCatalogo.Catalogo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoriasController(AppDbContext context, IConfiguration configuration, ILogger<CategoriasController> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        /*
        [HttpGet("LerArquivoConfiguracao")]
        public string GetValores()
        {
            var valor1 = _configuration["chave1"];
            var valor2 = _configuration["chave2"];

            var secao1 = _configuration ["secao1:chave2"];

            return $"Chave1 = {valor1} \nChave2 = {valor2} \nSeção1 => Chave2 = {secao1}";
        }*/

        [HttpGet("UsandoFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }


        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaModel>> GetCategoriasProdutos()
        {
            var categoriasProdutos = _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();
            if (categoriasProdutos is null)
            {
                _logger.LogWarning("Falha ao consultar todas as categorias");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }
            return categoriasProdutos;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<CategoriaModel>> Get()
        {
            var categorias = _context.Categorias.AsNoTracking().ToList(); //usa-se AsNoTracking apenas em consultas de leitura, para ter melhor

            if (categorias is null)
            {
                _logger.LogWarning("Falha ao consultar as categorias.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }
            return(categorias);  
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaModel> Get(int id)
        {

            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria == null)
            {
                _logger.LogWarning($"Categoria com o id = {id} não encontrada.");
                return NotFound($"Categoria com id = {id} não encontrada.");
            }
            return Ok(categoria);
        }


        [HttpPost]
        public ActionResult Post(CategoriaModel categoria)
        {
            if (categoria is null)
            {
                _logger.LogWarning($"Dados inválidos...");
                return BadRequest("Dados inválidos.");
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CategoriaModel categoria)
        {
            if (id != categoria.CategoriaId)
            {
                _logger.LogWarning($"O id = {id} é difrente da categoria!");
                return BadRequest("O id é diferente da categoria.");
            }

            _context.Entry(categoria).State = EntityState.Modified; //entity framework entende que foi modificada
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria == null)
            {
                _logger.LogWarning($"Categoria com o id = {id} não encotrada!");
                return NotFound($"Não foi localizar deletar a categoria id = {id}. Não encontrada.");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
}
