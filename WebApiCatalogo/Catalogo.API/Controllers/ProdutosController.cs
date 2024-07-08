using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiCatalogo.Catalogo.API.Pagination;
using WebApiCatalogo.Catalogo.Application.DTOs;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;
using WebApiCatalogo.Catalogo.Infrastucture.Repository;
using X.PagedList;

namespace WebApiCatalogo.Catalogo.API.Controllers
{
    [Route("[controller]")] // /produtos
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProdutosController(ILogger<ProdutosController> logger, IUnitOfWork uof, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }
        //---------------------------------------------------------------------------------//
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("/Produtos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            try
            {
                var produto = await _uof.ProdutoRepository.GetAllAsync();
                if (produto is null)
                {
                    _logger.LogWarning("Produtos não encontrados.");
                    return NotFound("Produtos não encontrados.");
                }

                //var destino = _mapper.Map<Destino>(Origem);
                var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

                return Ok(produtosDto);
            }
            catch (Exception)
            {
                _logger.LogWarning("Produtos não encontrados.");
                return BadRequest();
            }
        }
        //---------------------------------------------------------------------------------//
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosAsync(produtosParameters);
            return ObterProdutos(produtos);
        }
        //---------------------------------------------------------------------------------//

        [HttpGet("filter/preco/pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco produtosFilterParams)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFilterParams);
            return ObterProdutos(produtos);
        }

        private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(IPagedList<ProdutoModel> produtos)
        {
            var metaData = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.PageCount,
                produtos.TotalItemCount,
                produtos.HasNextPage,
                produtos.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }

        //---------------------------------------------------------------------------------//

        /// <summary>
        /// Exibe uma relação de produtos
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna uma lista de objetos Produto</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Policy = "UserOnly")]
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {

            var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);
            if (produto is null)
            {
                _logger.LogWarning($"O produto com o id {id} não foi encontrado.");
                return BadRequest($"Produto com o id = {id} não encontrado.");
            }

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }
        //---------------------------------------------------------------------------------//

        /// <summary>
        /// Obtem o produto pelo seu identificador id
        /// </summary>
        /// <param name="id">Código do produto</param>
        /// <returns>Um objeto produto</returns>
        /// 
        [Authorize(Policy = "UserOnly")]
        [HttpGet("/Produtos/Produtos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<ActionResult<ProdutoDTO>> GetId(int id)
        {
            var produto = await _uof.ProdutoRepository.GetProdutosPorCategoriaAsync(id);

            if (produto is null)
            {
                _logger.LogWarning($"O produto com o id {id} não foi encontrado.");
                return NotFound($"Produto com o id = {id} não encontrado.");
            }

            //var destino = _mapper.Map<Destino>(Origem);
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

            return Ok(produtosDto);
        }
        //---------------------------------------------------------------------------------//
        /// <summary>
        /// Inclui um novo produto
        /// </summary>
        /// <param name="produtoDto"></param>
        /// <returns>Retorna um novo produto</returns>
        [Authorize(Policy = "UserOnly")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDto)
        {

            if (produtoDto is null)
            {
                _logger.LogWarning("Produto não encontrado.");
                return BadRequest("Não encontrado.");
            }

            var produto = _mapper.Map<ProdutoModel>(produtoDto);

            var novoProduto = _uof.ProdutoRepository.Create(produto);
            await _uof.CommitAsync();

            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        //---------------------------------------------------------------------------------//
        [Authorize(Policy = "UserOnly")]
        [HttpPatch("{id}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ProdutoDTOUpdateResponse>> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
        {
            if (patchProdutoDTO is null || id <= 0)
            {
                _logger.LogWarning("Erro.");
                return BadRequest("Nullo ou id 0 não pode.");
            }

            var produto = await _uof.ProdutoRepository.GetAsync(c => c.ProdutoId == id);

            if (produto is null)
            {
                _logger.LogWarning("Produto não encontrado.");
                return NotFound($"Produto com o id = {id} não encontrado.");
            }

            var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

            patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

            if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            {
                _logger.LogWarning("Erro Patch.");
                return BadRequest("Nao esta dentro das regras");
            }

            _mapper.Map(produtoUpdateRequest, produto);

            _uof.ProdutoRepository.Update(produto);
            await _uof.CommitAsync();

            return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
        }
        //---------------------------------------------------------------------------------//
        [Authorize(Policy = "UserOnly")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                _logger.LogWarning($"ProdutoId: {id} deu erro. Id diferente.");
                return BadRequest($"Produto id = {id} é diferente.");
            }

            var produto = _mapper.Map<ProdutoModel>(produtoDto);

            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
            await _uof.CommitAsync();

            var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

            return Ok(produtoAtualizadoDto);
        }
        //---------------------------------------------------------------------------------//
        [Authorize(Policy = "UserOnly")]
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

            if (produto == null)
            {
                _logger.LogWarning($"O produto id {id} não foi localizado.");
                return NotFound($"Produto id = {id} não localizado.");
            }
            var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
            await _uof.CommitAsync();

            var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

            return Ok(produtoDeletadoDto);

        }
    }
}
