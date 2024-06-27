using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCatalogo.Catalogo.API.Controllers;
using WebApiCatalogo.Catalogo.Application.DTOs.Mappings;
using WebApiCatalogo.Catalogo.Infrastucture.Context;
using WebApiCatalogo.Catalogo.Infrastucture.Repository;

namespace WebApiCatalogoxUnitTests.Unit_Tests
{
    public class CategoriasUnitTestController
    {
        public IUnitOfWork repository;
        public IMapper mapper;
        public ILogger<CategoriasController> logger;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionString = "Server=localhost;Database=CatalogoDB;Uid=root;Pwd=1967;";

        static CategoriasUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        public CategoriasUnitTestController()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            logger = loggerFactory.CreateLogger<CategoriasController>();
 
            var context = new AppDbContext(dbContextOptions);
            repository = new UnitOfWork(context);
        }
    }
}
