using c19_38_BackEnd.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace c19_38_BackEnd.Datos
{
    public class DefaultContext : IdentityDbContext<Usuario,IdentityRole<int>,int>
    {

        public DbSet<Serie> Series { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<PlanDeEntrenamiento> PlanesDeEntrenamiento { get; set; }
        public DbSet<HistorialRendimiento> HistorialRendimientos { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<BibliotecaPlanUsuario> BibliotecaPlanUsuarios { get; set; }
        public DefaultContext(DbContextOptions options) : base(options)
        {
        }
    }
}
