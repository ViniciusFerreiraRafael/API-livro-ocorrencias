using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Models
{
    public class Ocorrencia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data da ocorrência é obrigatória.")]
        public DateTime DataOcorrencia { get; set; }

        // Preenchida automaticamente no momento do cadastro
        public DateTime DataRegistro { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string? Observacao { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        [MaxLength(30)]
        public string Status { get; set; }

        // FK para Aluno
        public int AlunoId { get; set; }

        // FK para Professor
        public int ProfessorId { get; set; }

        // FK para MotivoInfracao
        public int MotivoInfracaoId { get; set; }

        // Propriedades de navegação
        public virtual Aluno Aluno { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual MotivoInfracao MotivoInfracao { get; set; }

        public Ocorrencia() { } // Construtor exigido pelo EF
    }
}
