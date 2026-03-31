using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IClienteRepository
{
    public (int, int, bool, bool, IEnumerable<Ent_Cliente>) Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda);

}