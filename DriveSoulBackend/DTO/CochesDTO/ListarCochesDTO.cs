﻿namespace DriveSoulBackend.DTO.CochesDTO
{
        public class ListarCochesDTO
        {
            public int producto_id { get; set; }
            public int modelo_id { get; set; }
            public int anio { get; set; }
            public int kilometraje { get; set; }
            public string color { get; set; }
            public string tipo_combustible { get; set; }
            public string transmision { get; set; }

        }
}
