using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IContratoRepository
{
    public (int, int, bool, bool, IEnumerable<Ent_Contrato>) Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda);
    public (int, string) Crea(Ent_Contrato Contrato);
    public Ent_Contrato Obten_x_Id(int Con_Id);


}