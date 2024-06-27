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
using WebApiCatalogoxUnitTests.UnitTests;

namespace WebApiCatalogoxUnitTests.Unit_Tests
{
    public class DeleteProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public DeleteProdutoUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController((ILogger<ProdutosController>)controller.logger, controller.repository, controller.mapper);
        }


        //testes para delete

        [Fact]
        public async Task DeleteProdutoById_Return_OkResult()
        {
            //arrange
            var proId = 27;

            //ACT
            var result = await _controller.Delete(proId) as ActionResult<ProdutoDTO>;

            //Assert
            result.Should().NotBeNull(); // verifica se o objeto não é nulo
            result.Result.Should().BeOfType<OkObjectResult>(); //verifica se o resultado é OkResult
        }

        [Fact]
        public async Task DeleteProdutoById_Return_NotFound()
        {
            //arrange

            var proId = 999;

            //ACT
            var result = await _controller.Delete(proId) as ActionResult<ProdutoDTO>;

            //Assert
            result.Should().NotBeNull(); // verifica se o objeto não é nulo
            result.Result.Should().BeOfType<NotFoundObjectResult>(); //verifica se o resultado é NotFoundObjectResult
        }
    }
}
