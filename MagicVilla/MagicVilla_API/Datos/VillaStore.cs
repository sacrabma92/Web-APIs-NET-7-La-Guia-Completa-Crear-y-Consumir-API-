﻿using MagicVilla_API.Modelos.DTO;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
            new VillaDTO {Id=1, Nombre="Vista a la Piscina", Ocupantes = 1, MetrosCuadrados = 200},
            new VillaDTO {Id=2, Nombre="Vista a la Playa", Ocupantes = 3, MetrosCuadrados = 400},

            };
    }
}
