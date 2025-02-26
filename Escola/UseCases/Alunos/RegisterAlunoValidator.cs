using FluentValidation;
using Escola.Communication.Requests;

namespace Escola.Api.UseCases.Alunos
{
    public class RegisterAlunoValidator : AbstractValidator<RequestAlunoJson>
    {
        public RegisterAlunoValidator()
        {
            RuleFor(request => request.Nome)
                .NotEmpty()
                .WithMessage("Nome não pode ficar em branco!");

            RuleFor(request => request.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido.");

            RuleFor(request => request.DataNascimento)
                .NotEmpty()
                .WithMessage("A Data de Nascimento é obrigatória!");
        }
    }
}
