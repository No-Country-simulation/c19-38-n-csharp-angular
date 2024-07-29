namespace c19_38_BackEnd.Dtos
{
    public class PostDto
    {
        public int IdPost { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string? MediaUrl { get; set; }
        public int IdAutorUsuario { get; set; }
        public UsuarioPostComentarioDto Usuario {  get; set; }
        public List<ComentarioDto> Comentarios { get; set; }
    }

    public class UsuarioPostComentarioDto
    {
        public int Id {  get; set; }
        public string FullName { get; set; }
        public string MediaUrl { get; set; }
    }
}
