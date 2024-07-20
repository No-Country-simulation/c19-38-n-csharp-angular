using c19_38_BackEnd.Modelos;

namespace c19_38_BackEnd.Dtos
{
    public class RegistroDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmarContraseña { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Genero Genero { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
