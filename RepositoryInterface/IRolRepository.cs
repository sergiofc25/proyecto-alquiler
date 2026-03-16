using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IRolRepository
{
    IEnumerable<Ent_Rol> Obten();
}