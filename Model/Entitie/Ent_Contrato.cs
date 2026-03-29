using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitie
{
    public class Ent_Contrato
    {
        public int Con_Id { get; set; }
        public string? Con_Codigo { get; set; }
        public string? Con_Descripcion { get; set; }
        public DateTime? Con_FechaInicio { get; set; }
        public DateTime? Con_FechaFin { get; set; }
        public decimal? Con_PrecioAlqDefinido { get; set; }
        public bool Con_Estado { get; set; }
        public Ent_Usuario? eUsuario { get; set; }
        public Ent_Cliente? eCliente { get; set; }
        public Ent_Alojamiento? eAlojamiento { get; set; }
        public decimal? Con_Adelanto { get; set; }
        public decimal? Con_Garantia { get; set; }

    }
}
