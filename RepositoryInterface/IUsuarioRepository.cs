using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface IUsuarioRepository
{
    IEnumerable<Ent_Usuario> Obten();
    int Obten_Login(string Usu_Correo, string Usu_Clave);
    Ent_Usuario Obten_x_Correo(string Usu_Correo);
    Ent_Usuario Obten_Token_x_Correo(string Usu_Correo);
    bool Actualiza_Token(Ent_Usuario Usuario);

}