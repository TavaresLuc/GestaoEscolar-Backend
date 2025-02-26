using Escola.Api.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Matricula
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); // 

    public Guid AlunoId { get; set; } // 
    public required Aluno Aluno { get; set; } 

    public Guid CursoId { get; set; } 
    public required Curso Curso { get; set; }

    [JsonIgnore]
    public DateTime DataMatricula { get; set; } = DateTime.UtcNow;
}
