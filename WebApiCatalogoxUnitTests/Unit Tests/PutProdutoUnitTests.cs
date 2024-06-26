using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCatalogo.Catalogo.API.Controllers;
using WebApiCatalogo.Catalogo.Application.DTOs;
using WebApiCatalogo.Catalogo.Application.Services;
using WebApiCatalogoxUnitTests.UnitTests;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApiCatalogoxUnitTests.Unit_Tests
{
    public class PutProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public PutProdutoUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController((ILogger<ProdutosController>)controller.logger, controller.repository, controller.mapper);
        }

        //testes de unidade para put

        [Fact]
        public async Task PutProduto_Return_OkResult()
        {
            //Arrange
            var proId = 19;

            var updateProdutoDto = new ProdutoDTO()
            {
                ProdutoId = proId,
                Nome = "Produto Atualizado - Testes",
                Descricao = "Minha descricao",
                ImagemUrl = "testes.png",
                CategoriaId = 2
            };

            //Act

            var result = await _controller.Put(proId, updateProdutoDto) as ActionResult<ProdutoDTO>;

            //Assert

            result.Should().NotBeNull(); //Verifica se o resultado é nulo
            result.Result.Should().BeOfType<OkObjectResult>(); // verifica se o resultado é okobjectresult
        }

        [Fact]
        public async Task PutProduto_Return_BadRequest()
        {
            //Arrange
            var proId = 1000;

            var meuProduto = new ProdutoDTO()
            {
                ProdutoId = 19,
                Nome = "Produto Atualizado - Testes",
                Descricao = "Minha descricao",
                ImagemUrl = "testes.png",
                CategoriaId = 2
            };

            //Act

            var data = await _controller.Put(proId, meuProduto);

            //Assert

            data.Result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }
    }
}
