using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCatalogo.Catalogo.API.Controllers;
using WebApiCatalogoxUnitTests.UnitTests;
using Microsoft.Extensions.Configuration;
using WebApiCatalogo.Catalogo.Infrastucture.Repository;
using FluentAssertions;

namespace WebApiCatalogoxUnitTests.Unit_Tests
{
    public class GetCategoriaUnitTests  : IClassFixture<CategoriasUnitTestController>
    {
        private readonly CategoriasController _controller;

        public GetCategoriaUnitTests(CategoriasUnitTestController controller)
        {
            _controller = new CategoriasController(controller.logger, controller.repository);
        }

        //metodo para testes de get categorias

        [Fact]
        public async Task GetCategoriaById_OKResult()
        {
            //Arrange
            var categoriaId = 14;

            //Act
            var data = await _controller.Get(categoriaId);
              
            //assert (fluentAssertions)
            data.Result.Should().BeOfType<OkObjectResult>()//verifica se o resultado é do tipo okobjectresult
                .Which.StatusCode.Should().Be(200); //verifica se o codigo de status do okobjectresult é 200
        }

        [Fact]
        public async Task GetCategoriaById_ReturnNotFound()
        {
            //Arrange
            var categoriaId = 500;

            //Act
            var data = await _controller.Get(categoriaId);

            //assert (fluentAssertions)
            data.Result.Should().BeOfType<NotFoundObjectResult>()//verifica se o resultado é do tipo okobjectresult
                .Which.StatusCode.Should().Be(404); //verifica se o codigo de status do okobjectresult é notfound 404
        }

        [Fact]
        public async Task GetCategoriaAll_OkResult()
        { 
            //Act
            var data = await _controller.Get();

            //assert (fluentAssertions)
            data.Result.Should().BeOfType<OkObjectResult>()//verifica se o resultado é do tipo okobjectresult
                .Which.StatusCode.Should().Be(200); //verifica se o codigo de status do okobjectresult é 200
        }
    }
}
