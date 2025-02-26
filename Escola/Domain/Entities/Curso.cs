using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Escola.Api.Domain.Entities;

public class Curso
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Matricula> Matriculas { get; set; } = new();


}
