using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiCatalogo.Catalogo.Application.DTOs.Mappings;
using WebApiCatalogo.Catalogo.Application.Extensions;
using WebApiCatalogo.Catalogo.Application.Filters;
using WebApiCatalogo.Catalogo.Application.Interface;
using WebApiCatalogo.Catalogo.Application.Services;
using WebApiCatalogo.Catalogo.Infrastucture.Context;
using WebApiCatalogo.Catalogo.Infrastucture.Logging;
using WebApiCatalogo.Catalogo.Infrastucture.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles;
}).AddNewtonsoftJson();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection"); //string de conexao

var valor1 = builder.Configuration["chave1"];
var valor2 = builder.Configuration["secao1:chave2"];

//registro no container DI
builder.Services.AddDbContext<AppDbContext>(options => /* => é lambda*/
                     options.UseMySql(mySqlConnection,
                     ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddTransient<IMeuServico, MeuServico>();
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

builder.Services.AddAutoMapper(typeof(ProdutoDTOMappingProfile));

/*
builder.Services.AddLogging(builder =>
{
    builder.ClearProviders(); // Limpa os provedores de log existentes para evitar duplicações
    builder.AddConsole(); // Adiciona o provedor de log do console

    // Define os níveis de log desejados
    builder.AddFilter("Microsoft", LogLevel.Warning); // LogLevel.Warning ou superior para 'Microsoft' logs
    builder.AddFilter("System", LogLevel.Error); // LogLevel.Error ou superior para 'System' logs
    builder.AddFilter("MeuApp", LogLevel.Trace); // LogLevel.Trace ou superior para logs específicos do seu aplicativo
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
    {
        await next(context);
    });

app.MapControllers();

app.Run();
