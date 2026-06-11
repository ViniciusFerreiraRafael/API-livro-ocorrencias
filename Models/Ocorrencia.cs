using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Models
{
    public class Ocorrencia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data da ocorrência é obrigatória.")]
        public DateTime DataOcorrencia { get; set; }

        public DateTime DataRegistro { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string? Observacao { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        [MaxLength(30)]
        public string Status { get; set; }

        public int AlunoId { get; set; }
        public int ProfessorId { get; set; }
        public int MotivoInfracaoId { get; set; }

        public virtual Aluno? Aluno { get; set; }
        public virtual Professor? Professor { get; set; }
        public virtual MotivoInfracao? MotivoInfracao { get; set; }

        public Ocorrencia() { }
    }
}
