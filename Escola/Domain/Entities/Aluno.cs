using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escola.Api.Domain.Entities;

public class Aluno
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Column(TypeName = "DATE")]
    public DateOnly DataNascimento { get; set; }


    public List<Matricula> Matriculas { get; set; } = new();
}
