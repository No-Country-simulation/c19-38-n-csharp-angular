

using Microsoft.AspNetCore.Identity;

namespace c19_38_BackEnd.Modelos
{
    public class Usuario : IdentityUser<int>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaDeNac {  get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string MediaUrl { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQH23wl-q67cob4TWDwiHMie9RaSfX5A7Vm3tvs39u2KQ&s";
        public Disciplina Disciplina { get; set; }

    }
    public enum Genero
    {
        Masculino,
        Femenino
    }

    public enum Disciplina
    {
        Musculacion,
        Atletismo,
        Fuerza
    }
}
