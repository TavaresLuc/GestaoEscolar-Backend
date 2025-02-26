
namespace Escola.Communication.Requests
{
    public class RequestAlunoJson
    {
        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        
        public DateOnly DataNascimento { get; set; }
    }
}
