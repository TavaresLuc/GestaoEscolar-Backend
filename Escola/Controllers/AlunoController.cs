using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escola.Communication.Requests;
using Escola.Communication.Responses;
using Escola.Exception;
using Escola.Communication.Responses.Aluno;
using Escola.Api.Infrastructure.Data;
using Escola.Api.UseCases.Alunos;

namespace Escola.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly EscolaDbContext _context;
        private readonly RegisterAlunoUseCase _registerAlunoUseCase;
        private readonly AlunoValidacao _validacao;

        public AlunoController(EscolaDbContext context, RegisterAlunoUseCase registerAlunoUseCase, AlunoValidacao validacao)
        {
            _context = context;
            _registerAlunoUseCase = registerAlunoUseCase;
            _validacao = validacao;
        }

        [HttpPost]
        public IActionResult Create(RequestAlunoJson request)
        {
            try
            {
                var response = _registerAlunoUseCase.Execute(request); // 

                return Created(string.Empty, response);
            }
            catch (EscolaException ex)
            {
                return BadRequest(new ResponseErrorMessagesJson
                {
                    Errors = ex.GetErrorMessages()
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson
                {
                    Errors = new List<string> { "Erro Desconhecido." }
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseGetAlunos>>> GetAll()
        {

            try
            {

           
            var alunosLista = await _context.Alunos
                .Select(a => new ResponseGetAlunos
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Email = a.Email,
                    DataNascimento = a.DataNascimento
                })
                .ToListAsync();

            return Ok(alunosLista);
            }
            catch (EscolaException ex)
            {
                return BadRequest(new ResponseErrorMessagesJson

                {
                    Errors = ex.GetErrorMessages()
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson
                {
                    Errors = new List<string> { "Erro Desconhecido." }
                });

            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseGetAlunos>> GetById(Guid id)
        {
            try
            {
                var alunoById = await _context.Alunos.FindAsync(id);

                if (alunoById == null)
                    return NotFound();

                var response = alunoById;

                return Ok(response);
            }
            catch (EscolaException ex)
            {
                return BadRequest(new ResponseErrorMessagesJson

                {
                    Errors = ex.GetErrorMessages()
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson
                {
                    Errors = new List<string> { "Erro Desconhecido." }
                });

            }

        }

       [HttpPut("update/{id}")]
        public async Task<ActionResult<ResponseGetAlunos>> Update(Guid id, [FromBody] RequestAlunoJson request)
        {
            _validacao.ValidateAluno(request); 

            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
                return NotFound();

            aluno.Nome = request.Nome;
            aluno.Email = request.Email;

            await _context.SaveChangesAsync(); 

            var response = new ResponseGetAlunos
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
                DataNascimento = aluno.DataNascimento
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(id);

                if (aluno == null)
                    return NotFound();

                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (EscolaException ex)
            {
                return BadRequest(new ResponseErrorMessagesJson

                {
                    Errors = ex.GetErrorMessages()
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson
                {
                    Errors = new List<string> { "Erro Desconhecido." }
                });

            }
        }

    }
}
