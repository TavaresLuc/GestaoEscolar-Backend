using FluentValidation;
using Escola.Communication.Requests;
using Escola.Exception;

namespace Escola.Api.UseCases.Alunos
{
    public class AlunoValidacao
    {
        private readonly RegisterAlunoValidator _validator;

        public AlunoValidacao()
        {
            _validator = new RegisterAlunoValidator();
        }

        public void ValidateAluno(RequestAlunoJson request)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                // 🔹 Adicionamos log para depuração
                Console.WriteLine("Validação falhou! Erros:");
                foreach (var error in errorMessages)
                {
                    Console.WriteLine($"- {error}");
                }

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
