using System.ComponentModel.DataAnnotations;

namespace VillaMagica_API.Modelos.DTO
{
    public class NumeroVillaDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }
    }
}
