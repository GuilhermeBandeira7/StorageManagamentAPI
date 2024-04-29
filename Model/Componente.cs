using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNGeneratorAPI.Model
{
    public class Componente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public int Ncm { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public bool Enabled { get; set; } = true;

        [ForeignKey("OperacaoId")]
        public Operacao? Operacao { get; set; }
    }
}
