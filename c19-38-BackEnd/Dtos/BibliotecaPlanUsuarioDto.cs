using c19_38_BackEnd.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

namespace c19_38_BackEnd.Dtos
{
    public class BibliotecaPlanUsuarioDto
    {
        public int IdBiblioteca { get; set; }
        public int IdPlan { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaGuardado { get; set; }
    }
}
