using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IUsuarioService
{
    Task<IEnumerable<Ent_Usuario>> Obten();
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
}