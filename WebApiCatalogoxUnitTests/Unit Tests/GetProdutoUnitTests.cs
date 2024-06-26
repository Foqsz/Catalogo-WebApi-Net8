using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCatalogo.Catalogo.API.Controllers;
using WebApiCatalogoxUnitTests.UnitTests;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace WebApiCatalogoxUnitTests.Unit_Tests
{
    public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public GetProdutoUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController((ILogger<ProdutosController>)controller.logger, controller.repository, controller.mapper);
        }

        [Fact]
        public async Task GetProdutoById_OkResult()
        {
            //Arrange
            var produtoId = 2;

            //Act
            var data = await _controller.Get(produtoId);

            //Assert(xunit)
            //var okResult = Assert.IsType<OkObjectResult>(data.Result);
           // Assert.Equal(200, okResult.StatusCode);

            //assert (fluentAssertions)
            data.Result.Should().BeOfType<OkObjectResult>()//verifica se o resultado é do tipo okobjectresult
                .Which.StatusCode.Should().Be(200); //verifica se o codigo de status do okobjectresult é 200

        }
    }
}
