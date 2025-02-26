using Escola.Api.Domain.Entities;
using Escola.Api.Infrastructure.Data;
using Escola.Communication.Requests;
using Escola.Communication.Responses.Aluno;
using Escola.Api.UseCases.Alunos;

namespace Escola.Api.UseCases.Alunos
{
    public class RegisterAlunoUseCase
    {
        private readonly EscolaDbContext _dbContext;

        public RegisterAlunoUseCase(EscolaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResponseRegisteredAlunoJson Execute(RequestAlunoJson request)
        {

            var validator = new AlunoValidacao();
            validator.ValidateAluno(request);

            var entity = new Aluno
            {
                Email = request.Email,
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
            };

            _dbContext.Alunos.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredAlunoJson
            {
                Nome = entity.Nome
            };
        }
    }
}
