using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCatalogo.Catalogo.API.Controllers;
using WebApiCatalogo.Catalogo.Application.DTOs;

namespace WebApiCatalogoxUnitTests.Unit_Tests
{
    public class PostsCategoriaUnitTests : IClassFixture<CategoriasUnitTestController>
    {
        private readonly CategoriasController _controller;

        public PostsCategoriaUnitTests(CategoriasUnitTestController controller)
        {
            _controller = new CategoriasController(controller.logger, controller.repository);
        }

        //metodo de testes para POST

        [Fact]
        public async Task PostCategoria_Return_CreatedStatusCodes()
        {
            //Arrange
            var novaCategoriaDto = new CategoriaDTO()
            {
                CategoriaId = 0,
                Nome = "New Categoria",
                ImagemUrl = "imagemfake.png"
            };

            //Act
            var data = await _controller.Post(novaCategoriaDto);

            //Assert
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(201);

        }
    }
}
