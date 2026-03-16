using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IUsuarioRepository
{
    IEnumerable<Ent_Usuario> Obten();
}