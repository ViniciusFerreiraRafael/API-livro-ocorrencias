using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Data;
using ProjetoAPI.Models;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurmasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
        {
            return await _context.Turmas.Include(t => t.Alunos).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurma(int id)
        {
            var turma = await _context.Turmas
                .Include(t => t.Alunos)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (turma == null)
                return NotFound("Turma não encontrada.");

            return Ok(turma);
        }

        [HttpPost]
        public async Task<ActionResult<Turma>> PostTurma(Turma turma)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Turmas.Add(turma);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTurma), new { id = turma.Id }, turma);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurma(int id, Turma turma)
        {
            if (id != turma.Id)
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = await _context.Turmas.AnyAsync(t => t.Id == id);
            if (!existe)
                return NotFound("Turma não encontrada.");

            try
            {
                _context.Entry(turma).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
                return NotFound("Turma não encontrada.");

            try
            {
                _context.Turmas.Remove(turma);
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
