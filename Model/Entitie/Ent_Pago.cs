using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitie
{
    public class Ent_Pago
    {
        public int Pag_Id { get; set; }
        public DateTime? Pag_FechaPago { get; set; }
        public DateTime? Pag_FechaVencimieto { get; set; }
        public DateTime? Pag_FechaPagoRealizado { get; set; }
        public decimal? Pag_Cantidad { get; set; }
        public Ent_Tipo_Pago? eTipo_Pago { get; set; }
        public Ent_Contrato? eContrato { get; set; }
        public bool Pag_Estado { get; set; }
        public string? Pag_Codigo { get; set; }

    }
}
