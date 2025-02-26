using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escola.Api.Infrastructure.Data;
using Escola.Api.Domain.Entities;
using Escola.Communication.Responses;
using Escola.Exception;


namespace Escola.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly EscolaDbContext _context;

        public CursoController(EscolaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Curso request)
        {
            if (request == null)
                return BadRequest(new ResponseErrorMessagesJson { Errors = new List<string> { "Curso não pode ser nulo." } });

            // Validação: garantir que nome e descrição sejam fornecidos
            if (string.IsNullOrEmpty(request.Nome))
                return BadRequest(new ResponseErrorMessagesJson { Errors = new List<string> { "Nome do curso é obrigatório." } });

            if (string.IsNullOrEmpty(request.Descricao))
                return BadRequest(new ResponseErrorMessagesJson { Errors = new List<string> { "Descrição do curso é obrigatória." } });

            // Salva o curso no banco de dados
            _context.Cursos.Add(request);
            await _context.SaveChangesAsync();

            // Retorna o curso criado
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetAll()
        {
            var cursos = await _context.Cursos.ToListAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetById(Guid id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return NotFound();

            return Ok(curso);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Curso request)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return NotFound();

            curso.Nome = request.Nome;
            curso.Descricao = request.Descricao;
            await _context.SaveChangesAsync();

            return Ok(curso);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return NotFound();

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }


}
