using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IAlojamientoService
{
    Task<IEnumerable<Ent_Alojamiento>> Obten();
    Task<string> Actualiza(Ent_Alojamiento Alojamiento);
    Task<Ent_Alojamiento> Obten_x_Id(int Alo_Id);


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
    public async Task<string> Actualiza(Ent_Alojamiento Alojamiento)
    {
        using var context = _unitOfWork.Create();

        var MensajeError = context.Repositories.AlojamientoRepository.Actualiza(Alojamiento);

        //if (MensajeError == string.Empty)
        //{
        //    context.SaveChanges();
        //}
        if (string.IsNullOrEmpty(MensajeError))
        {
            context.SaveChanges();
        }
        return MensajeError;
    }
    public async Task<Ent_Alojamiento> Obten_x_Id(int Alo_Id)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.AlojamientoRepository.Obten_x_Id(Alo_Id);
        });
    }
}