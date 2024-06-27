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
    public class PutCategoriaUnitTests : IClassFixture<CategoriasUnitTestController>
    {
        private readonly CategoriasController _controller;

        public PutCategoriaUnitTests(CategoriasUnitTestController controller)
        {
            _controller = new CategoriasController(controller.logger, controller.repository);
        }

        [Fact]
        public async Task PutCategoria_Return_UpdateOk()
        {
            //Arrange
            var categoriaId = 999;

            var updateCategoriaDto = new CategoriaDTO()
            {
                CategoriaId = 999,
                Nome = "New Categoria",
                ImagemUrl = "imagemfake.png"
            };

            //Act

            var result = await _controller.Put(categoriaId, updateCategoriaDto) as ActionResult<CategoriaDTO>;

            //Assert

            result.Should().NotBeNull(); //Verifica se o resultado é nulo
            result.Result.Should().BeOfType<OkObjectResult>(); // verifica se o resultado é okobjectresult
        }

        [Fact]
        public async Task PutCategoria_Return_ErrorBadRequest()
        {
            //Arrange
            var categoriaId = 999;

            var updateCategoriaDto = new CategoriaDTO()
            {
                CategoriaId = 123,
                Nome = "New Categoria",
                ImagemUrl = "imagemfake.png"
            };

            //Act

            var result = await _controller.Put(categoriaId, updateCategoriaDto) as ActionResult<CategoriaDTO>;

            //Assert

            result.Should().NotBeNull(); //Verifica se o resultado é nulo
            result.Result.Should().BeOfType<BadRequestObjectResult>(); // verifica se o resultado é badrequest
        }
    }
}
