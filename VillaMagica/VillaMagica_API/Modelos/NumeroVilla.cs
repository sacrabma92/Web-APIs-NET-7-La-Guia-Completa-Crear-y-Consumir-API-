﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaMagica_API.Modelos
{
    public class NumeroVilla
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }
        public string DetalleEspecial { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        [Required]
        public int VillaId { get; set; }
        [ForeignKey("VillaId")]
        public Villa Villa { get; set; }
    }
}