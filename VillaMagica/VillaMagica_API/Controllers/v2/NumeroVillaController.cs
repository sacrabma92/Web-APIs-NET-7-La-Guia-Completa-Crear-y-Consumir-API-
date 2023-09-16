using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using VillaMagica_API.Datos;
using VillaMagica_API.Modelos;
using VillaMagica_API.Modelos.DTO;
using VillaMagica_API.Repositorio.IRepositorio;

namespace VillaMagica_API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/numeroVillas")]
    [ApiVersion("2.0")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroVillaController(
            ILogger<NumeroVillaController> logger, 
            IVillaRepositorio villaRepo, 
            INumeroVillaRepositorio numeroRepo,
            IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response = new();
        }


        // API de prueba de versionamiento a v2
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetNumeroVillasV2()
        {
            try
            {
                IEnumerable<NumeroVilla> numeroVillaList = await _numeroRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDTO>>(numeroVillaList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
