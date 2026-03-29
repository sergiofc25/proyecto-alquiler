using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IPagoRepository
{
    public (int, int, bool, bool, IEnumerable<Ent_Pago>) Obten_Paginado(int RegistroPagina, int NumeroPagina, string? PorNombre);

}