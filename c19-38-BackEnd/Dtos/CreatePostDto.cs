namespace c19_38_BackEnd.Dtos
{
    public class CreatePostDto
    {
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public IFormFile? MediaUrl { get; set; }
        public int IdAutorUsuario { get; set; }
    }
}
