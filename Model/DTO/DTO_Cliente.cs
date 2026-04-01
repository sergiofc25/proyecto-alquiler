using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DTO_Cliente_Obten_Paginado
    {
        public string? IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? NumDocumento { get; set; }
        public string? FotoDocumento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? TipoDocumento { get; set; }
        public bool EstadoCliente { get; set; }
    }

    public class DTO_Cliente_Obten_x_NumDocumento
    {
        public string? NombreCliente { get; set; }
        public string? NumDocumento { get; set; }
        public string? FotoDocumento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? TipoDocumento { get; set; }
        public bool EstadoCliente { get; set; }
    }
}
