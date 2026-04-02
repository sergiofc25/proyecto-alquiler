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
    public class DTO_Contrato_Crea
    {
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NumDocumento { get; set; }
        public string? AlojamientoCodigo { get; set; }
        public string? NombreCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? EmailCliente { get; set; }
        public string? TipoDocumento { get; set; }
    }
    public class DTO_Contrato_Obten_x_Id
    {
        public string? IdContrato { get; set; }
        public string? ContratoCodigo { get; set; }
        public string? Descripcion { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public string? PrecioAlquiler { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NombreCliente { get; set; }
        public string? NumDocumento { get; set; }
        public string? AlojamientoCodigo { get; set; }
        public string? ContratoEstado { get; set; }
    }
}
