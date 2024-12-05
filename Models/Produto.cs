using System.ComponentModel.DataAnnotations;

namespace caotinhofeliz.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 10000, ErrorMessage = "O preço deve ser entre 0,01 e 10.000")]
        public decimal Preco { get; set; }

        [StringLength(255, ErrorMessage = "O caminho da imagem deve ter no máximo 255 caracteres")]
        public string? ImagemUrl { get; set; } // Caminho ou URL da imagem
    }
}