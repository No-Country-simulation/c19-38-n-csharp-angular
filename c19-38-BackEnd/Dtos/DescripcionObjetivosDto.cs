using c19_38_BackEnd.Modelos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace c19_38_BackEnd.Dtos
{
    public class DescripcionObjetivosDto
    {
        public bool EsEntrenador { get; set; }
        public string Motivacion { get; set; }
        public string MayorObstaculo { get; set; }
        public LugarEntrenamiento LugarEntrenamiento { get; set; }
        public PreferenciaHora PreferenciaHora { get; set; }
        public NivelActividadFisica ActividadFisica { get; set; }
    }
}
