using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DTO_ResponsePag<T>
    {
        public int PaginaActual { get; set; }
        public int TotalDePagina { get; set; }
        public int ElementosPorPagina { get; set; }
        public int TotalDeElementos { get; set; }
        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}
