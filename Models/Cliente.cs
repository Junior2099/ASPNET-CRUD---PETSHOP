using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace caotinhofeliz.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, ErrorMessage = "CPF inválido")]
        public string CPF { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; } = string.Empty;

        // Relacionamento um para muitos com Pet
        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}