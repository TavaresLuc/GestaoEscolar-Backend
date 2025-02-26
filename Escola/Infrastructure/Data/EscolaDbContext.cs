using Microsoft.EntityFrameworkCore;
using Escola.Api.Domain.Entities;

namespace Escola.Api.Infrastructure.Data
{
    public class EscolaDbContext : DbContext
    {
        // 🔹 Construtor CORRETO para permitir injeção de dependência
        public EscolaDbContext(DbContextOptions<EscolaDbContext> options) : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        // 🔹 O `OnConfiguring` só será usado se o `DbContext` não for registrado no `Program.cs`
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Evita configurar duplicado
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BaseEscola;User Id=sa;Password=qaz@123;TrustServerCertificate=True;");
            }
        }
    }
}
