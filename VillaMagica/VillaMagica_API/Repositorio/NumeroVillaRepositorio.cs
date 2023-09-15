using VillaMagica_API.Datos;
using VillaMagica_API.Modelos;
using VillaMagica_API.Repositorio.IRepositorio;

namespace VillaMagica_API.Repositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public NumeroVillaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;

            _db.NumeroVillas .Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
