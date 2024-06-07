using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCatalogo.Catalogo.API.Pagination;
using WebApiCatalogo.Catalogo.Application.DTOs;
using WebApiCatalogo.Catalogo.Core.Model;
using WebApiCatalogo.Catalogo.Infrastucture.Context;
using WebApiCatalogo.Catalogo.Infrastucture.Repository;

namespace WebApiCatalogo.Catalogo.API.Controllers
{
    [Route("[controller]")] // /produtos
    [ApiController]
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

        [HttpGet("/Produtos")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            var produto = _uof.ProdutoRepository.GetAll().ToList();
            if (produto is null)
            {
                _logger.LogWarning("Produtos não encontrados.");
                return NotFound("Produtos não encontrados.");
            }

            //var destino = _mapper.Map<Destino>(Origem);
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

            return Ok(produtosDto);
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters);

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {

            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
            if (produto is null)
            {
                _logger.LogWarning($"O produto com o id {id} não foi encontrado.");
                return NotFound($"Produto com o id = {id} não encontrado.");
            }

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }

        [HttpGet("/Produtos/Produtos/{id}")]
        public ActionResult<ProdutoDTO> GetId(int id)
        { 
            var produto = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

            if (produto is null)
            {
                _logger.LogWarning($"O produto com o id {id} não foi encontrado.");
                return NotFound($"Produto com o id = {id} não encontrado.");
            }

            //var destino = _mapper.Map<Destino>(Origem);
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

            return Ok(produtosDto);
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
        {

            if (produtoDto is null)
            {
                _logger.LogWarning("Produto não encontrado.");
                return BadRequest("Não encontrado.");
            }

            var produto = _mapper.Map<ProdutoModel>(produtoDto);

            var novoProduto = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();

            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ProdutoDTOUpdateResponse> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
        {
            if (patchProdutoDTO is null || id <= 0)
            {
                _logger.LogWarning("Erro.");
                return BadRequest("Nullo ou id 0 não pode.");
            }

            var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

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
            _uof.Commit();

            return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                _logger.LogWarning($"ProdutoId: {id} deu erro. Id diferente.");
                return BadRequest($"Produto id = {id} é diferente.");
            }

            var produto = _mapper.Map<ProdutoModel>(produtoDto);

            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

            return Ok(produtoAtualizadoDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto == null)
            {
                _logger.LogWarning($"O produto id {id} não foi localizado.");
                return NotFound($"Produto id = {id} não localizado.");
            }
            var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

            return Ok(produtoDeletadoDto);

        }
    }
}
