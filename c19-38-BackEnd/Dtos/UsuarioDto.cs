using c19_38_BackEnd.Modelos;

namespace c19_38_BackEnd.Dtos
{
    public class UsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaDeNac { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public NivelActividadFisica ActividadFisica { get; set; }
        public string? MediaUrl { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
