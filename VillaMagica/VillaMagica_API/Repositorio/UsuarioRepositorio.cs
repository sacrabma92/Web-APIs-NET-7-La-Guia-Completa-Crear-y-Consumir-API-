using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VillaMagica_API.Datos;
using VillaMagica_API.Modelos;
using VillaMagica_API.Modelos.DTO;
using VillaMagica_API.Repositorio.IRepositorio;

namespace VillaMagica_API.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;

        public UsuarioRepositorio(ApplicationDbContext db,
               IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSetting:Secret");
        }
        public bool IsUsuarioUnico(string userName)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());

            if(usuario == null )
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower() &&
                                                                    x.Password == loginRequestDTO.Password);

            if(usuario == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    Usuario = null
                };
            }
            // Si usuario Existe Generamos el JW Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = usuario
            };
            return loginResponseDTO;
        }

        public async Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO)
        {
            Usuario usuario = new()
            {
                UserName = registroRequestDTO.UserName,
                Password = registroRequestDTO.Password,
                Nombres = registroRequestDTO.Nombres,
                Rol = registroRequestDTO.Rol,
            };
            await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();
            usuario.Password = "";
            return usuario;
        }
    }
}
