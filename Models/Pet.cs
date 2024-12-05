using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace caotinhofeliz.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do pet é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A espécie é obrigatória")]
        [StringLength(50, ErrorMessage = "A espécie deve ter no máximo 50 caracteres")]
        public string Especie { get; set; } = string.Empty;

        [Required(ErrorMessage = "O cliente é obrigatório")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; } // Permitir nulo para evitar validação indevida
    }
}