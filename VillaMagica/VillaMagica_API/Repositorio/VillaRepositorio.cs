using VillaMagica_API.Datos;
using VillaMagica_API.Modelos;
using VillaMagica_API.Repositorio.IRepositorio;

namespace VillaMagica_API.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public VillaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;

            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
