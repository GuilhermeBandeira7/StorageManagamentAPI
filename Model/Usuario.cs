using System.ComponentModel.DataAnnotations;

namespace SNGeneratorAPI.Model
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Login { get; set; } = string.Empty;
        [Required]
        public string Senha { get; set; } = string.Empty;

        public List<Operacao>? Operacoes { get; set; } = new List<Operacao>();
    }
}
