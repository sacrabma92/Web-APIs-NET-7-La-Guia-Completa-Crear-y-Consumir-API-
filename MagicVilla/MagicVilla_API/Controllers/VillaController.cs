using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        public VillaController()
        {

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVilla()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVillaPorID(int id)
        {
            if(id == 0) { return BadRequest(); }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            if(villa == null) {  return NotFound(); }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CrearVilla([FromBody] VillaDTO villaDTO)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            if(VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDTO.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese Nombre Ya Existe");
                return BadRequest(ModelState);
            }

            if(villaDTO == null) { return BadRequest(villaDTO);  }

            if(villaDTO.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

            villaDTO.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            VillaStore.villaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new {id = villaDTO.Id}, villaDTO);

        }

        [HttpPut("{id:int}")]
        public IActionResult PutVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.Id) { return BadRequest(); }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            villa.Nombre = villaDTO.Nombre;
            villa.Ocupantes = villaDTO.Ocupantes;
            villa.MetrosCuadrados = villaDTO.MetrosCuadrados;

            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public IActionResult PatchVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null ) { return BadRequest(); }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            patchDTO.ApplyTo(villa, ModelState);

            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0) { return BadRequest(); }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            if(villa == null) { return NotFound(); }

            VillaStore.villaList.Remove(villa);

            return NoContent();
        }
    }
}
