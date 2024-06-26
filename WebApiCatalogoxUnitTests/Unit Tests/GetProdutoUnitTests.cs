using WebApiCatalogo.Catalogo.API.Controllers;
using WebApiCatalogoxUnitTests.UnitTests;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using WebApiCatalogo.Catalogo.Application.DTOs;

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
        public async Task GetProdutoById_OKResult()
        {
            //Arrange
            var produtoId = 19;

            //Act
            var data = await _controller.Get(produtoId);

            //Assert(xunit)
            //var okResult = Assert.IsType<OkObjectResult>(data.Result);
           // Assert.Equal(200, okResult.StatusCode);

            //assert (fluentAssertions)
            data.Result.Should().BeOfType<OkObjectResult>()//verifica se o resultado é do tipo okobjectresult
                .Which.StatusCode.Should().Be(200); //verifica se o codigo de status do okobjectresult é 200
        }

        [Fact]
        public async Task GetProdutoById_Return_NotFound()
        {
            //Arrange
            var produtoId = 999;

            //Act
            var data = await _controller.Get(produtoId); 

            //assert (fluentAssertions)
            data.Result.Should().BeOfType<NotFoundObjectResult>()//verifica se o resultado é do tipo okobjectresult
                .Which.StatusCode.Should().Be(404); //verifica se o codigo de status do okobjectresult é 200

        }

        [Fact]
        public async Task GetProdutoById_Return_BadRequest()
        {
            //Arrange
            var produtoId = -1;

            //Act
            var data = await _controller.Get(produtoId);

            //assert (fluentAssertions)
            data.Result.Should().BeOfType<BadRequestObjectResult>()//verifica se o resultado é do tipo okobjectresult
                .Which.StatusCode.Should().Be(400); //verifica se o codigo de status do okobjectresult é 200

        }

        [Fact]
        public async Task GetProdutoById_Return_ListOfProdutos()
        { 

            //Act
            var data = await _controller.GetProdutos();

            //assert (fluentAssertions)
            data.Result.Should().BeOfType<OkObjectResult>()//verifica se o resultado é do tipo okobjectresult
                .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>().And.NotBeNull();

        }

        [Fact]
        public async Task GetProdutoById_Return_BadRequestResult()
        {

            //Act
            var data = await _controller.GetProdutos();

            //assert (fluentAssertions)
            data.Result.Should().BeOfType<BadRequestResult>(); 

        }
    }
}
