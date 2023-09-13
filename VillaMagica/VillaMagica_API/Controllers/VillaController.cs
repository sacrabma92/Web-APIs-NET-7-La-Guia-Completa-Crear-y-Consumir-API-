using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaMagica_API.Datos;
using VillaMagica_API.Modelos;
using VillaMagica_API.Modelos.DTO;
using VillaMagica_API.Repositorio.IRepositorio;

namespace VillaMagica_API.Controllers
{
    [Route("api/villas")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> vilaList = await _villaRepo.ObtenerTodos();
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(vilaList));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.Obtener(x => x.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CrearVilla([FromBody] CrearVillaDTO crearVillaDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(await _villaRepo.Obtener(v => v.Nombre.ToLower() == crearVillaDTO.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese Nombre ya existe!");
                return BadRequest(ModelState);
            }
            if(crearVillaDTO == null)
            {
                return BadRequest(crearVillaDTO);
            }
            Villa modelo = _mapper.Map<Villa>(crearVillaDTO);

            await _villaRepo.Crear(modelo);

            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.Obtener(x => x.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            _villaRepo.Remover(villa);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            if(updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }

            Villa modelo = _mapper.Map<Villa>(updateDTO);

            _villaRepo.Actualizar(modelo);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO) 
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.Obtener(x => x.Id == id, tracked: false);

            // Traemos los datos de la tabla villa que queremos actualizar. Primero
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null) return BadRequest();

            patchDTO.ApplyTo(villaDTO, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaDTO);

            _villaRepo.Actualizar(modelo);
            return NoContent();

        }
    }
}
