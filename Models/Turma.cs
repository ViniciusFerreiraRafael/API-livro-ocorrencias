using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da turma é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O ano letivo é obrigatório.")]
        public int AnoLetivo { get; set; }

        [Required(ErrorMessage = "O turno é obrigatório.")]
        [MaxLength(20)]
        public string Turno { get; set; }

        // Propriedade de navegação: 1 Turma -> N Alunos
        public virtual ICollection<Aluno> Alunos { get; set; }

        public Turma() { } // Construtor exigido pelo EF
    }
}
