using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitie
{
    public class Ent_Cliente
    {
        public int Cli_Id { get; set; }
        public string? Cli_Nombre { get; set; }
        public string? Cli_NumDocumento { get; set; }
        public string? Cli_FotoDocumento { get; set; }
        public string? Cli_NumTelefono { get; set; }
        public string? Cli_Email { get; set; }
        public bool Cli_Estado { get; set; }
        public Ent_Tipo_Documento? eTipo_Documento { get; set; }

    }
}
