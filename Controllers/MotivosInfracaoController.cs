using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Data;
using ProjetoAPI.Models;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotivosInfracaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotivosInfracaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotivoInfracao>>> GetMotivosInfracao()
        {
            return await _context.MotivosInfracao
                .Include(m => m.Ocorrencias)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotivoInfracao>> GetMotivoInfracao(int id)
        {
            var motivo = await _context.MotivosInfracao
                .Include(m => m.Ocorrencias)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motivo == null)
                return NotFound("Motivo de infração não encontrado.");

            return Ok(motivo);
        }

        [HttpPost]
        public async Task<ActionResult<MotivoInfracao>> PostMotivoInfracao(MotivoInfracao motivo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.MotivosInfracao.Add(motivo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMotivoInfracao), new { id = motivo.Id }, motivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotivoInfracao(int id, MotivoInfracao motivo)
        {
            if (id != motivo.Id)
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = await _context.MotivosInfracao.AnyAsync(m => m.Id == id);
            if (!existe)
                return NotFound("Motivo de infração não encontrado.");

            try
            {
                _context.Entry(motivo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotivoInfracao(int id)
        {
            var motivo = await _context.MotivosInfracao.FindAsync(id);

            if (motivo == null)
                return NotFound("Motivo de infração não encontrado.");

            try
            {
                _context.MotivosInfracao.Remove(motivo);
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
