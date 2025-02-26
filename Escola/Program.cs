using Escola.Api.Infrastructure.Data;
using Escola.Api.UseCases.Alunos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configura o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem (pode restringir para o dom�nio do seu front-end em produ��o)
              .AllowAnyMethod()  // Permite qualquer m�todo HTTP (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader(); // Permite qualquer cabe�alho HTTP
    });
});

// Adiciona os servi�os necess�rios para os controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configura o serializador JSON para preservar as refer�ncias e evitar ciclos
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        // Defina a profundidade m�xima, se necess�rio
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// Swagger e outros servi�os
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura��o do contexto do banco de dados com SQL Server
builder.Services.AddDbContext<EscolaDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configura��o de depend�ncias
builder.Services.AddScoped<RegisterAlunoUseCase>();
builder.Services.AddScoped<AlunoValidacao>();

var app = builder.Build();

// Aplicando a pol�tica de CORS no pipeline
app.UseCors("AllowAll"); // Usando a pol�tica definida acima

// Configura��o do Swagger para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapear os controladores
app.MapControllers();

app.Run();
