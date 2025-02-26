

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Escola.Communication.Responses.Aluno
{
    public class ResponseGetAlunos
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Column(TypeName = "DATE")]
        public DateOnly DataNascimento { get; set; }

    }
}
