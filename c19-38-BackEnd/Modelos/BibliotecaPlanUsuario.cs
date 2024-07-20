using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c19_38_BackEnd.Modelos
{
    public class BibliotecaPlanUsuario
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBiblioteca { get; set; }
        public int IdPlan { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaGuardado { get; set; }
        [ForeignKey(nameof(IdPlan))]
        public PlanDeEntrenamiento PlanDeEntrenamiento {  get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }
    }
}
