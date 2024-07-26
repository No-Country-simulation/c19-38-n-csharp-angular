using Azure.Core;
using Azure;
using c19_38_BackEnd.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
        public DbSet<DescripcionObjetivos> DescripcionObjetivos { get; set; }
        public DefaultContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsAssignableFrom(typeof(Serie)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(Post)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(Comentario)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(PlanDeEntrenamiento)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(HistorialRendimiento)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(Ejercicio)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(BibliotecaPlanUsuario)) ||
                    entityType.ClrType.IsAssignableFrom(typeof(DescripcionObjetivos))
                    )
                {
                    foreach (var relationship in entityType.GetForeignKeys())
                    {
                        relationship.DeleteBehavior = DeleteBehavior.Restrict;
                    }
                }
            }
        }
    }
}
