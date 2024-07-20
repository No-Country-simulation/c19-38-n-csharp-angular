using c19_38_BackEnd.Modelos;

namespace c19_38_BackEnd.Dtos
{
    public class PlanDeEntrenamientoDto
    {
        public int IdPlan { get; set; }
        public string Descripcion { get; set; }
        public Disciplina TipoDisciplina { get; set; }
        public Nivel Nivel { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int IdAutorUsuario { get; set; }
    }
}
