using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DTO_Boleta_Obten_x_Pago
    {
        public string? IdBoleta { get; set; }
        public string? Codigo { get; set; }
        public string? fecha { get; set; }
        public string? descripcion { get; set; }
        public string? total { get; set; }
        public string? IdPago { get; set; }
        public string? NombreCliente { get; set; }
        public string? NumDocumento { get; set; }
        public bool? EstadoPago { get; set; }
    }
}
