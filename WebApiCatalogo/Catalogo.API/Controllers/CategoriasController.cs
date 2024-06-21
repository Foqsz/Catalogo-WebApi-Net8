using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiCatalogo.Catalogo.API.Pagination;
using WebApiCatalogo.Catalogo.Application.DTOs;
using WebApiCatalogo.Catalogo.Application.DTOs.Mappings;
using WebApiCatalogo.Catalogo.Application.Filters;
using WebApiCatalogo.Catalogo.Application.Interface;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;
using WebApiCatalogo.Catalogo.Infrastucture.Repository;
using X.PagedList;

namespace WebApiCatalogo.Catalogo.API.Controllers
{ 
    [Route("[controller]")]
    [ApiController]
    [EnableRateLimiting("fixedwindow")]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        //---------------------------------------------------------------------------------//
        public CategoriasController(IConfiguration configuration, ILogger<CategoriasController> logger, IUnitOfWork uof)
        {
            _configuration = configuration;
            _logger = logger;
            _uof = uof;
        }
        //---------------------------------------------------------------------------------//
        /*
        [HttpGet("LerArquivoConfiguracao")]
        public string GetValores()
        {
            var valor1 = _configuration["chave1"];
            var valor2 = _configuration["chave2"];

            var secao1 = _configuration ["secao1:chave2"];

            return $"Chave1 = {valor1} \nChave2 = {valor2} \nSeção1 => Chave2 = {secao1}";
        }*/
        //---------------------------------------------------------------------------------//
        [HttpGet("UsandoFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        //---------------------------------------------------------------------------------//
        //Listar Categorias
        /*[HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaModel>> GetCategoriasProdutos()
        {
            var categoriasProdutos = _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();
            if (categoriasProdutos is null)
            {
                _logger.LogWarning("Falha ao consultar todas as categorias");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }
            return categoriasProdutos;
        }*/
        //---------------------------------------------------------------------------------//
        [HttpGet]
        //[Authorize]
        [DisableRateLimiting]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _uof.CategoriaRepository.GetAllAsync();

            if (categorias is null)
            {
                _logger.LogWarning("Falha ao consultar as categorias.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação.");
            }

            var categoriasDto = categorias.ToCategoriaDTOList();

            return Ok(categoriasDto);
        }

        //---------------------------------------------------------------------------------//
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = await _uof.CategoriaRepository.GetCategoriasAsync(categoriasParameters);

            return ObterCategorias(categorias);
        }
        //---------------------------------------------------------------------------------//

        [HttpGet("filter/nome/pagination")]

        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaNome([FromQuery] CategoriasFiltroNome categoriasFiltroParams)
        {
            var categorias = await _uof.CategoriaRepository.GetProdutoNomeAsync(categoriasFiltroParams);
            return ObterCategorias(categorias);
        }

        //---------------------------------------------------------------------------------//
        private ActionResult<IEnumerable<CategoriaDTO>> ObterCategorias(IPagedList<CategoriaModel> categorias)
        {
            var metaData = new
            {
                categorias.Count,
                categorias.PageSize,
                categorias.PageCount,
                categorias.TotalItemCount,
                categorias.HasNextPage,
                categorias.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));

            var categoriasDto = categorias.ToCategoriaDTOList();
            return Ok(categoriasDto);
        }

        //---------------------------------------------------------------------------------//
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {

            var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com o id = {id} não encontrada.");
                return NotFound($"Categoria com id = {id} não encontrada.");
            }

            var categoriaDto = categoria.ToCategoriaDTO();

            return Ok(categoriaDto);
        }

        //---------------------------------------------------------------------------------//
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
            {
                _logger.LogWarning($"Dados inválidos...");
                return BadRequest("Dados inválidos.");
            }

            var categoria = categoriaDto.ToCategoria();

            var categoriaCriada = _uof.CategoriaRepository.Create(categoria); //create é feito na memória então não precisa do await
            await _uof.CommitAsync();

            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();

            return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
        }
        //---------------------------------------------------------------------------------//
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                _logger.LogWarning($"O id = {id} é difrente da categoria!");
                return BadRequest("O id é diferente da categoria.");
            }

            var categoria = categoriaDto.ToCategoria();

            var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria); //update é feito na memória então não precisa do await
            await _uof.CommitAsync();

            var categoriaAtualizadaDto = categoriaAtualizada.ToCategoriaDTO();

            return Ok(categoriaAtualizadaDto);
        }
        //---------------------------------------------------------------------------------//
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

            if (categoria == null)
            {
                _logger.LogWarning($"Categoria com o id = {id} não encotrada!");
                return NotFound($"Não foi possivel deletar a categoria id = {id}. Não encontrada.");
            }
            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria); //Delete é feito na memória então não precisa do await
            await _uof.CommitAsync();

            var categoriaExcluidaDto = categoriaExcluida.ToCategoriaDTO();

            return Ok(categoriaExcluidaDto);
        //---------------------------------------------------------------------------------//
        }
    }
}
