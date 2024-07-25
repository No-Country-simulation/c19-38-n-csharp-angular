using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace c19_38_BackEnd.Modelos
{
    public class Ejercicio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEjercicio { get; set; }
        public string Descripcion { get; set; }
        public string MusculoPrincipal { get; set; }
        public string MusculoSecundario { get; set; }
        public string MediaUrl { get; set; }
    }
}
