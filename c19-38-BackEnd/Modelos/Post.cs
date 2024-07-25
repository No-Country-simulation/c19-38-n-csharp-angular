using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace c19_38_BackEnd.Modelos
{
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPost { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string? MediaUrl { get; set; }
        public int IdAutorUsuario { get; set; }
        [ForeignKey(nameof(IdAutorUsuario))]
        public Usuario AutorUsuario{ get; set;}
    }

}
