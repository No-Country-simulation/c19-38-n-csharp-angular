namespace c19_38_BackEnd.Dtos
{
    public class ComentarioDto
    {
        public int IdComentario { get; set; }
        public string Cuerpo { get; set; }
        public int IdPost { get; set; }
        public int IdAutor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public UsuarioPostComentarioDto Usuario {  get; set; }
    }

    public class CreateComentarioDto
    {
        public string Cuerpo { get; set; }
        public int IdPost { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
