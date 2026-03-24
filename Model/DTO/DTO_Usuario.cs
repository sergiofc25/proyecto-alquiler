using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DTO_Usuario_Obten
    {
        public string? NombreUsuario { get; set; }
        public string? Email { get; set; }
        public string? Rol { get; set; }
    }
    public class DTO_Usuario_Obten_Login
    {
        public string? Usu_Correo { get; set; }
        public string? Usu_Clave { get; set; }
    }
}
