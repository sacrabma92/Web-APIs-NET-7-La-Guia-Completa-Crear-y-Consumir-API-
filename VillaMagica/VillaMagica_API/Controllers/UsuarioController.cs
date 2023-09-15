using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaMagica_API.Modelos;
using VillaMagica_API.Modelos.DTO;
using VillaMagica_API.Repositorio.IRepositorio;

namespace VillaMagica_API.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        protected APIResponse _apiResponse;

        public UsuarioController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _apiResponse = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO modelo)
        {
            var loginResponse = await _usuarioRepo.Login(modelo);

            if (loginResponse.Usuario == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages.Add("UserName o Password son Incorrectos");
                return BadRequest(_apiResponse);
            }
            _apiResponse.IsExitoso = true;
            _apiResponse.statusCode = HttpStatusCode.OK;
            _apiResponse.Resultado = loginResponse;
            return Ok(_apiResponse);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO modelo)
        {
            bool isUsuarioUnico = _usuarioRepo.IsUsuarioUnico(modelo.UserName);

            if (!isUsuarioUnico)
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages.Add("Usuario ya Existe!");
                return BadRequest(_apiResponse);
            }
            var usuario = await _usuarioRepo.Registrar(modelo);

            if(usuario == null)
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMessages.Add("Error al registrar Usuario!");
                return BadRequest(_apiResponse);
            }
            _apiResponse.IsExitoso = true;
            _apiResponse.statusCode = HttpStatusCode.OK;
            return Ok(_apiResponse);
        }
    }
}
