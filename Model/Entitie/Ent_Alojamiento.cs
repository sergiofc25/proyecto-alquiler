using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitie
{
    public class Ent_Alojamiento
    {
        public int Alo_Id { get; set; }
        public string? Alo_Codigo { get; set; }
        public string? Alo_Descripcion { get; set; }
        public decimal Alo_Precio { get; set; }
        public decimal? Alo_Garantia { get; set; }
        public bool Alo_BanIndependiente { get; set; }
        public bool Alo_Amoblado { get; set; }
        public bool Alo_Estado { get; set; }

    }
}
