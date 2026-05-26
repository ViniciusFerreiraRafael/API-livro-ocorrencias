using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do professor é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A disciplina é obrigatória.")]
        [MaxLength(60)]
        public string Disciplina { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [MaxLength(20)]
        public string Matricula { get; set; }

        // Propriedade de navegação: 1 Professor -> N Ocorrencias
        public virtual ICollection<Ocorrencia> Ocorrencias { get; set; }

        public Professor() { } // Construtor exigido pelo EF
    }
}
