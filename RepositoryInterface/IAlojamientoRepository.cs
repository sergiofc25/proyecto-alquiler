using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IAlojamientoRepository
{
    IEnumerable<Ent_Alojamiento> Obten();
}