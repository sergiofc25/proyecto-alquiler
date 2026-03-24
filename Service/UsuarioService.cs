using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IUsuarioService
{
    Task<IEnumerable<Ent_Usuario>> Obten();
    Task<int> Obten_Login(string Usu_Correo, string Usu_Clave);
    Task<Ent_Usuario> Obten_x_Correo(string Usu_Correo);
    Task<Ent_Usuario> Obten_Token_x_Correo(string Usu_Correo);
    Task<bool> Actualiza_Token(Ent_Usuario Usuario);

}

public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Ent_Usuario>> Obten()
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.UsuarioRepository.Obten();
        });
    }
    public async Task<bool> Actualiza_Token(Ent_Usuario Usuario)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            bool Afectado;

            Afectado = context.Repositories.UsuarioRepository.Actualiza_Token(Usuario);

            if (Afectado)
            {
                context.SaveChanges();

                return true;
            }

            return false;
        });
    }

    public async Task<int> Obten_Login(string Usu_Correo, string Usu_Clave)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.UsuarioRepository.Obten_Login(Usu_Correo, Usu_Clave);
        });
    }

    public async Task<Ent_Usuario> Obten_x_Correo(string Usu_Correo)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.UsuarioRepository.Obten_x_Correo(Usu_Correo);
        });
    }
    public async Task<Ent_Usuario> Obten_Token_x_Correo(string Usu_Correo)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.UsuarioRepository.Obten_Token_x_Correo(Usu_Correo);
        });
    }
}