

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
        public NivelActividadFisica ActividadFisica {  get; set; }
        public string? MediaUrl { get; set; }
        public Disciplina Disciplina { get; set; }

    }
    public enum Genero
    {
        Masculino,
        Femenino
    }

    public enum NivelActividadFisica
    {
        Sedentario,
        Ligero,
        Moderado,
        Activo,
        MuyActivo
    }

    public enum Disciplina
    {
        Musculacion,
        Atletismo,
        Fuerza
    }
}
