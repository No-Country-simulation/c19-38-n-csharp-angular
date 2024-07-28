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
    }
}
