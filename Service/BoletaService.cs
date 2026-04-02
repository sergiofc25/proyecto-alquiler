using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IBoletaService
{

    Task<Ent_Boleta> Obten_x_Pago(int Pag_Id);
}

public class BoletaService : IBoletaService
{
    private readonly IUnitOfWork _unitOfWork;

    public BoletaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Ent_Boleta> Obten_x_Pago(int Pag_Id)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.BoletaRepository.Obten_x_Pago(Pag_Id);
        });
    }
}