using Microsoft.EntityFrameworkCore;
using VillaMagica_API.Modelos;

namespace VillaMagica_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base ( options )
        {
            
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumeroVilla> NumeroVillas { get; set; }
    }
}
