using Escola.Api.Domain.Entities;
using Escola.Api.Infrastructure.Data;
using Escola.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escola.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        private readonly EscolaDbContext _context;

        public MatriculaController(EscolaDbContext context)
        {
            _context = context;
        }

        // POST: api/matricula
        [HttpPost]
        public async Task<IActionResult> Matricular([FromBody] RequestMatriculaJson request)
        {
            if (request == null)
                return BadRequest(new ResponseErrorMessagesJson { Errors = new List<string> { "A matrícula não pode ser nula." } });

            // Verificar se o aluno existe
            var aluno = await _context.Alunos.FindAsync(request.AlunoId);
            if (aluno == null)
                return NotFound(new ResponseErrorMessagesJson { Errors = new List<string> { "Aluno não encontrado." } });

            // Verificar se o curso existe
            var curso = await _context.Cursos.FindAsync(request.CursoId);
            if (curso == null)
                return NotFound(new ResponseErrorMessagesJson { Errors = new List<string> { "Curso não encontrado." } });

            // Verificar se a matrícula já existe (um aluno não pode se matricular no mesmo curso mais de uma vez)
            var matriculaExistente = await _context.Matriculas
                .AnyAsync(m => m.AlunoId == request.AlunoId && m.CursoId == request.CursoId);

            if (matriculaExistente)
                return BadRequest(new ResponseErrorMessagesJson { Errors = new List<string> { "O aluno já está matriculado neste curso." } });

            // Criar a matrícula com as entidades resolvidas manualmente
            var matricula = new Matricula
            {
                AlunoId = request.AlunoId,
                CursoId = request.CursoId,
                Aluno = aluno,
                Curso = curso,
                DataMatricula = DateTime.UtcNow
            };

            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            // Retornar a matrícula criada
            return CreatedAtAction(nameof(GetMatriculaById), new { id = matricula.Id }, matricula);
        }

        // GET: api/matricula/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Matricula>> GetMatriculaById(Guid id)
        {
            var matricula = await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (matricula == null)
                return NotFound();

            return Ok(matricula);
        }

        // GET: api/matricula/aluno/{alunoId}
        [HttpGet("aluno/{alunoId}")]
        public async Task<ActionResult<IEnumerable<Matricula>>> GetMatriculasByAluno(Guid alunoId)
        {
            var matriculas = await _context.Matriculas
                .Where(m => m.AlunoId == alunoId)
                .Include(m => m.Curso)
                .ToListAsync();

            if (!matriculas.Any())
                return NotFound(new ResponseErrorMessagesJson { Errors = new List<string> { "Nenhuma matrícula encontrada para esse aluno." } });

            return Ok(matriculas);
        }

        // GET: api/matricula/curso/{cursoId}
        [HttpGet("curso/{cursoId}")]
        public async Task<ActionResult<IEnumerable<Matricula>>> GetMatriculasByCurso(Guid cursoId)
        {
            var matriculas = await _context.Matriculas
                .Where(m => m.CursoId == cursoId)
                .Include(m => m.Aluno) // Inclui os dados do aluno na resposta
                .ToListAsync();

            if (!matriculas.Any())
                return NotFound(new ResponseErrorMessagesJson { Errors = new List<string> { "Nenhuma matrícula encontrada para esse curso." } });

            return Ok(matriculas);
        }


        // DELETE: api/matricula/remover/{alunoId}/{cursoId}
        [HttpDelete("remover/{alunoId}/{cursoId}")]
        public async Task<IActionResult> RemoverMatricula(Guid alunoId, Guid cursoId)
        {
            // Verificar se a matrícula existe
            var matricula = await _context.Matriculas
                .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

            if (matricula == null)
                return NotFound(new ResponseErrorMessagesJson { Errors = new List<string> { "Matrícula não encontrada." } });

            // Remover a matrícula
            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
