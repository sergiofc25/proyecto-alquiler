using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DTO_Contrato_Obten_Paginado
    {
        public string? IdContrato { get; set; }
        public string? ContratoCodigo { get; set; }
        public string? DescripcionContrato { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public string? PrecioAlquiler { get; set; }
        public bool? EstadoContrato { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NombreCliente { get; set; }
        public string? NumDocumento { get; set; }
        public string? AlojamientoCodigo { get; set; }
        public string? AdelantoC { get; set; }
        public string? GarantiaC { get; set; }
    }
}
