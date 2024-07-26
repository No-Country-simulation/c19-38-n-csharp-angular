using c19_38_BackEnd.Modelos;

namespace c19_38_BackEnd.Dtos
{
    public class DescripcionObjetivosDto
    {
        public int IdDescripcion { get; set; }
        public int IdUsuario { get; set; }
        public string Motivacion { get; set; }
        public string MayorObstaculo { get; set; }
        public LugarEntrenamiento LugarEntrenamiento { get; set; }
        public string EquiposEnCasa { get; set; }
        public string Objetivo { get; set; }
        public PreferenciaHora PreferenciaHora { get; set; }
        public NivelActividadFisica ActividadFisica { get; set; }
    }
}
