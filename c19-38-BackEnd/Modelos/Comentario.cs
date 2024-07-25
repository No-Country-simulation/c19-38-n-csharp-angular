using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c19_38_BackEnd.Modelos
{
    public class Comentario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComentario { get; set; }
        public string Cuerpo { get; set; }
        public int IdPost { get; set; }
        public int IdAutor { get; set; }
        public DateTime FechaPublicacion { get; set; }

        [ForeignKey(nameof(IdPost))]
        public Post Post { get; set; }
        [ForeignKey(nameof(IdAutor))]
        public Usuario Autor { get; set; }

    }
}
