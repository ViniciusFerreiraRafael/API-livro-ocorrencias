using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Data;
using ProjetoAPI.Models;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Alunos
                .Include(a => a.Turma)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
                return NotFound("Aluno não encontrado.");

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool turmaExiste = await _context.Turmas.AnyAsync(t => t.Id == aluno.TurmaId);
            if (!turmaExiste)
                return NotFound("Turma informada não encontrada.");

            try
            {
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id)
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = await _context.Alunos.AnyAsync(a => a.Id == id);
            if (!existe)
                return NotFound("Aluno não encontrado.");

            try
            {
                _context.Entry(aluno).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
                return NotFound("Aluno não encontrado.");

            try
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }
    }
}
