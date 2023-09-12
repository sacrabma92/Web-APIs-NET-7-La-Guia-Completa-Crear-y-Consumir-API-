using System.ComponentModel.DataAnnotations;

namespace VillaMagica_API.Modelos.DTO
{
    public class CrearVillaDTO
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }
        [Required]
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
