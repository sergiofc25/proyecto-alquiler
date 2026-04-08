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
        public string? Email { get; set; }
        public string? Pass { get; set; }
    }
    public class DTO_Usuario_Obten_x_Correo
    {
        public string? Email { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Rol { get; set; }
        public string? FechaRegistroUsuario { get; set; }
        public string? EstadoUsuario { get; set; }
    }
}
