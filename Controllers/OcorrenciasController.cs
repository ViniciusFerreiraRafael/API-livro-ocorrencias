using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Data;
using ProjetoAPI.Models;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcorrenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OcorrenciasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ocorrencia>>> GetOcorrencias()
        {
            return await _context.Ocorrencias
                .Include(o => o.Aluno)
                    .ThenInclude(a => a.Turma)
                .Include(o => o.Professor)
                .Include(o => o.MotivoInfracao)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ocorrencia>> GetOcorrencia(int id)
        {
            var ocorrencia = await _context.Ocorrencias
                .Include(o => o.Aluno)
                    .ThenInclude(a => a.Turma)
                .Include(o => o.Professor)
                .Include(o => o.MotivoInfracao)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ocorrencia == null)
                return NotFound("Ocorrência não encontrada.");

            return Ok(ocorrencia);
        }

        [HttpPost]
        public async Task<ActionResult<Ocorrencia>> PostOcorrencia(Ocorrencia ocorrencia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool alunoExiste = await _context.Alunos.AnyAsync(a => a.Id == ocorrencia.AlunoId);
            if (!alunoExiste)
                return NotFound("Aluno informado não encontrado.");

            bool professorExiste = await _context.Professores.AnyAsync(p => p.Id == ocorrencia.ProfessorId);
            if (!professorExiste)
                return NotFound("Professor informado não encontrado.");

            bool motivoExiste = await _context.MotivosInfracao.AnyAsync(m => m.Id == ocorrencia.MotivoInfracaoId);
            if (!motivoExiste)
                return NotFound("Motivo de infração informado não encontrado.");

            try
            {
                ocorrencia.DataRegistro = DateTime.Now;

                _context.Ocorrencias.Add(ocorrencia);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOcorrencia), new { id = ocorrencia.Id }, ocorrencia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOcorrencia(int id, Ocorrencia ocorrencia)
        {
            if (id != ocorrencia.Id)
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = await _context.Ocorrencias.AnyAsync(o => o.Id == id);
            if (!existe)
                return NotFound("Ocorrência não encontrada.");

            try
            {
                _context.Entry(ocorrencia).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOcorrencia(int id)
        {
            var ocorrencia = await _context.Ocorrencias.FindAsync(id);

            if (ocorrencia == null)
                return NotFound("Ocorrência não encontrada.");

            try
            {
                _context.Ocorrencias.Remove(ocorrencia);
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
