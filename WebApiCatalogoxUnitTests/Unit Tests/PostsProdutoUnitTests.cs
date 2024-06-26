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
    public class PostsProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public PostsProdutoUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController((ILogger<ProdutosController>)controller.logger, controller.repository, controller.mapper);
        }

        //metodo de testes para POST

        [Fact]
        public async Task PostProduto_Return_CreatedStatusCodes()
        {
            //Arrange
            var novoProdutoDto = new ProdutoDTO()
            {
                Nome = "Novo Produto",
                Descricao = "Descricao do novo produto",
                Preco = 10.99m,
                ImagemUrl = "imagemfake.png",
                CategoriaId = 2
            };

            //Act
            var data = await _controller.Post(novoProdutoDto);

            //Assert
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task PostProduto_Return_BadRequest()
        {
            //Arrange
            ProdutoDTO prod = null;

            //Act
            var data = await _controller.Post(prod);

            //Assert
            var badRequestResult = data.Result.Should().BeOfType<BadRequestObjectResult>();
            badRequestResult.Subject.StatusCode.Should().Be(400);
        }
    }
}
