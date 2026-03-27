using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IAlojamientoService
{
    Task<IEnumerable<Ent_Alojamiento>> Obten();
}

public class AlojamientoService : IAlojamientoService
{
    private readonly IUnitOfWork _unitOfWork;

    public AlojamientoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Ent_Alojamiento>> Obten()
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.AlojamientoRepository.Obten();
        });
    }
}