using AutoMapper;
using VillaMagica_API.Modelos;
using VillaMagica_API.Modelos.DTO;

namespace VillaMagica_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, CrearVillaDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }
    }
}
