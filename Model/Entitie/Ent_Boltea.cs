using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitie
{
    public class Ent_Boleta
    {
        public int Bol_Id { get; set; }
        public string? Bol_Codigo { get; set; }
        public DateOnly? Bol_Fecha { get; set; }
        public string? Bol_Descripcion { get; set; }
        public decimal? Bol_Total { get; set; }
        public Ent_Pago ePago { get; set; }
        public bool Bol_Estado { get; set; }
        public DateOnly? Bol_FechaFin { get; set; }
        public DateOnly? Bol_FechaPagoRealizado { get; set; }

    }
}
