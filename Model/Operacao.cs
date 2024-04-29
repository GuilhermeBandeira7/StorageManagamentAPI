using System.ComponentModel.DataAnnotations;

namespace SNGeneratorAPI.Model
{
    public class Operacao
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.Now.ToLocalTime();
        public int NotaFiscal { get; set; }
        public int Status { get; set; }
        public List<Componente> Componentes { get; set; } = new List<Componente>();
    }
}
