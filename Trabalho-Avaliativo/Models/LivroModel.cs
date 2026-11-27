using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    // Cada propriedade representa uma coluna da tabela "livro"
    public class LivroModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o ISBN")]
        [StringLength(20, ErrorMessage = "O ISBN deve ter no máximo 20 caracteres")]
        public string Isbn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o título do livro")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Informe o valor")]
        [Range(0.01, 999999.99, ErrorMessage = "Informe um valor maior que zero")]
        public decimal Valor { get; set; }
    }
}