using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Registro de Aluno (RA) é obrigatório.")]
        [MaxLength(20)]
        public string RegistroAluno { get; set; }

        [Required(ErrorMessage = "O nome do aluno é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O contato do responsável é obrigatório.")]
        [MaxLength(20)]
        public string ContatoResponsavel { get; set; }

        public int TurmaId { get; set; }

        public virtual Turma Turma { get; set; }

        public virtual ICollection<Ocorrencia> Ocorrencias { get; set; }

        public Aluno() { }
    }
}
