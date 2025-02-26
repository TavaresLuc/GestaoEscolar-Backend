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
        policy.AllowAnyOrigin()  // Permite qualquer origem (pode restringir para o domínio do seu front-end em produção)
              .AllowAnyMethod()  // Permite qualquer método HTTP (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader(); // Permite qualquer cabeçalho HTTP
    });
});

// Adiciona os serviços necessários para os controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configura o serializador JSON para preservar as referências e evitar ciclos
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        // Defina a profundidade máxima, se necessário
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// Swagger e outros serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configuração do contexto do banco de dados com SQL Server
builder.Services.AddDbContext<EscolaDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuração de dependências
builder.Services.AddScoped<RegisterAlunoUseCase>();
builder.Services.AddScoped<AlunoValidacao>();

var app = builder.Build();

// Aplicando a política de CORS no pipeline
app.UseCors("AllowAll"); // Usando a política definida acima

// Configuração do Swagger para desenvolvimento
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
