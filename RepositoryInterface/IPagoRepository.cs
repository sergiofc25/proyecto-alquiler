using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IPagoRepository
{
    public (int, int, bool, bool, IEnumerable<Ent_Pago>) Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda);
    public (int, int, bool, bool, IEnumerable<Ent_Pago>) Obten_Paginado_x_Contrato(int RegistroPagina, int NumeroPagina, string? TerBusqueda, string Con_Id);
    public string Actualiza_Pagar(int Pag_Id, DateOnly FechaPagoRealizado);

}