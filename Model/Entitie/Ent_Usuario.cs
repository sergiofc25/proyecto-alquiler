using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitie
{
    public class Ent_Usuario
    {
        public int Usu_Id { get; set; }
        public string? Usu_Nombre { get; set; }
        public string? Usu_Correo { get; set; }
        public bool Usu_Estado { get; set; }
        public Ent_Rol? eRol { get; set; } = new();
        public string? Usu_Pass { get; set; }
        public string? Usu_Salt { get; set; }
        public DateTime Usu_FechaHoraRegistro { get; set; }
        public string? Usu_Observacion { get; set; }
        public string? Usu_Token { get; set; }
        public string? Usu_TokenActualizado { get; set; }
        public string Usu_Clave { get; set; }
        public DateTime Usu_FecHoraTokenActualizado { get; set; }
        
    }
}
