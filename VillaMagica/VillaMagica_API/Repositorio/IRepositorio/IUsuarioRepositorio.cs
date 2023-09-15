using VillaMagica_API.Modelos;
using VillaMagica_API.Modelos.DTO;

namespace VillaMagica_API.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        //Metodo que verifica que no exista el mismo usuario
        bool IsUsuarioUnico(string userName);
        // Metodo de login, devuelve LoginResponseDTO e ingresa LoginRequestDTO
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        //Metodo para registrar un usuario
        Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO);
    }
}
