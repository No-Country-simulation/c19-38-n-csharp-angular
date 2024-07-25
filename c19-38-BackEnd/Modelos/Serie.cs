using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c19_38_BackEnd.Modelos
{
    public class Serie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSerie { get; set; }
        public string Descripcion { get; set; }
        public int CantidadSeries { get; set; }
        public int? CantidadRepeticiones { get; set; }
        public DateTime TiempoDescanso { get; set; }
        public DateTime? Duracion { get; set; }
        public int IdPlan { get; set; }
        public int IdEjercicio { get; set; }
        [ForeignKey(nameof(IdPlan))]
        public PlanDeEntrenamiento Plan {  get; set; }
        [ForeignKey(nameof(IdEjercicio))]
        public Ejercicio Ejercicio {  set; get; }
    }
}
