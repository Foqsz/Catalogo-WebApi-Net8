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
    public class DeleteCategoriaUnitTests : IClassFixture<CategoriasUnitTestController>
    {
        private readonly CategoriasController _controller;

        public DeleteCategoriaUnitTests(CategoriasUnitTestController controller)
        {
            _controller = new CategoriasController(controller.logger, controller.repository);
        }

        [Fact]
        public async Task DeleteCategoria_Return_Ok()
        {
            var categoriaId = 123;

            //ACT
            var result = await _controller.Delete(categoriaId) as ActionResult<CategoriaDTO>;

            //Assert
            result.Should().NotBeNull(); // verifica se o objeto não é nulo
            result.Result.Should().BeOfType<OkObjectResult>(); //verifica se o resultado é OkResult
        }

        [Fact]
        public async Task DeleteCategoria_Return_NotFound()
        {
            var categoriaId = 12312;

            //ACT
            var result = await _controller.Delete(categoriaId) as ActionResult<CategoriaDTO>;

            //Assert
            result.Should().NotBeNull(); // verifica se o objeto não é nulo
            result.Result.Should().BeOfType<NotFoundObjectResult>(); //verifica se o resultado é OkResult
        }
    }
}
