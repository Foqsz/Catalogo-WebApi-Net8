using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiCatalogo.Catalogo.Application.Extensions;
using WebApiCatalogo.Catalogo.Application.Filters;
using WebApiCatalogo.Catalogo.Application.Interface;
using WebApiCatalogo.Catalogo.Application.Services;
using WebApiCatalogo.Catalogo.Infrastucture.Context; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
