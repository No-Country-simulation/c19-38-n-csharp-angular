using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c19_38_BackEnd.Modelos
{
    public class DescripcionObjetivos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDescripcion { get; set; }
        public int IdUsuario { get; set; }
        public string Motivacion { get; set; }
        public string MayorObstaculo { get; set; }
        public LugarEntrenamiento LugarEntrenamiento { get; set; }
        public string EquiposEnCasa { get; set; }
        public string Objetivo { get; set; }
        public PreferenciaHora PreferenciaHora { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }
        public NivelActividadFisica ActividadFisica { get; set; }
    }

    public enum PreferenciaHora
    {
        Mañana,
        Tarde,
        Noche,
        Indiferente
    }
    public enum LugarEntrenamiento
    {
        Casa,
        Gimnasio,
        Parque
    }
    public enum NivelActividadFisica
    {
        Sedentario,
        Ligero,
        Moderado,
        Activo,
        MuyActivo
    }

}
