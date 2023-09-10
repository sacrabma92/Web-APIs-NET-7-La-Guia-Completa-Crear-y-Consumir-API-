using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaMagica_API.Modelos;
using VillaMagica_API.Modelos.DTO;

namespace VillaMagica_API.Controllers
{
    [Route("api/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return new List<VillaDTO>
            {
                new VillaDTO { Id = 1, Nombre = "Vista a la Piscina"},
                new VillaDTO { Id = 2, Nombre = "Vista a la playa"},
            };
        }
    }
}
