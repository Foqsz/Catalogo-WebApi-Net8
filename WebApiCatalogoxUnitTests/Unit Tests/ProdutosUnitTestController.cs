using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCatalogo.Catalogo.Application.DTOs.Mappings;
using WebApiCatalogo.Catalogo.Infrastucture.Context;
using WebApiCatalogo.Catalogo.Infrastucture.Repository;

namespace WebApiCatalogoxUnitTests.UnitTests;

public class ProdutosUnitTestController
{
    public IUnitOfWork repository;
    public IMapper mapper;
    public ILogger logger;
    public static DbContextOptions<AppDbContext> dbContextOptions { get; }

    public static string connectionString = "Server=localhost;DataBase=apicatalogodb;Uid=root;Pwd=Hw8vup5e;";

    static ProdutosUnitTestController()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
    }

    public ProdutosUnitTestController()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProdutoDTOMappingProfile());
        });

        mapper = config.CreateMapper();
        var context = new AppDbContext(dbContextOptions);
        repository = new UnitOfWork(context);
    }
}
