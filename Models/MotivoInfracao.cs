using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Models
{
    public class MotivoInfracao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição do motivo é obrigatória.")]
        [MaxLength(200)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A gravidade é obrigatória.")]
        [MaxLength(20)]
        public string Gravidade { get; set; }

        public virtual ICollection<Ocorrencia>? Ocorrencias { get; set; }

        public MotivoInfracao() { }
    }
}
