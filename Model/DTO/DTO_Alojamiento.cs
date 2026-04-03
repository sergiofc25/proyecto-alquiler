using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DTO_Alojamiento_Obten
    {
        public string? IdAlojamiento { get; set; }
        public string? CodigoAlojamiento { get; set; }
        public string? Descripcion { get; set; }
        public string? Precio { get; set; }
        public string? Garantia { get; set; }
        public bool? BanIndependiente { get; set; }
        public bool? Amoblado { get; set; }
        public bool EstadoAlojamiento { get; set; }
    }
    public class DTO_Alojamiento_Obten_x_Id
    {
        public string? IdAlojamiento { get; set; }
        public string? CodigoAlojamiento { get; set; }
        public string? Descripcion { get; set; }
        public string? Precio { get; set; }
        public string? Garantia { get; set; }
        public bool? BanIndependiente { get; set; }
        public bool? Amoblado { get; set; }
        public bool EstadoAlojamiento { get; set; }
    }
    public class DTO_Alojamiento_Actualiza
    {
        public string? Descripcion { get; set; }
        public string? Precio { get; set; }
        public string? Garantia { get; set; }
    }
}
