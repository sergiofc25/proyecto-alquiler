using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DTO_Pago_Obten_Paginado
    {
        public string? IdPago { get; set; }
        public string? CodigoPago { get; set; }
        public string? FechaPago { get; set; }
        public string? FechaVencimiento { get; set; }
        public string? FechaPagado { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NombreCliente { get; set; }
        public string? DNICliente { get; set; }
        public string? CodigoHabitacion { get; set; }
        public string? CodigoContrato { get; set; }
        public string? PagoCantidad { get; set; }
        public string? TipoPago { get; set; }
        public bool? EstadoPago { get; set; }
    }
}
