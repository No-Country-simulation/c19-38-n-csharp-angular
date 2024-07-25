using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c19_38_BackEnd.Modelos
{
    public class HistorialRendimiento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHistorial { get; set; }
        public int cantFlexionesMin { get; set; }
        public int cantSentadillasMin { get; set; }
        public DateTime CienMtsTiempo { get; set; }
        public DateTime FechaGuardado { get; set; }
        public string? Nota { get; set; }
        public int IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }
    }
}
