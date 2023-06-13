using MagicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre = "Villa Real",
                    Detalle = "Detalle de la villa...",
                    ImangeUrl = "",
                    Ocupantes = 4,
                    MetrosCuadrados = 100,
                    Tarifa = 150,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                },               
                new Villa()
                {
                    Id = 2,
                    Nombre = "Villa Vista Plana",
                    Detalle = "Detalle de la en el fondo...",
                    ImangeUrl = "",
                    Ocupantes = 2,
                    MetrosCuadrados = 60,
                    Tarifa = 86,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                }
            );
        }
    }
}
