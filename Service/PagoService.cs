using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IPagoService
{
    Task<(int, int, bool, bool, IEnumerable<Ent_Pago>)> Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda);
    Task<string> Actualiza_Pagar(int Pag_Id);

}

public class PagoService : IPagoService
{
    private readonly IUnitOfWork _unitOfWork;

    public PagoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(int, int, bool, bool, IEnumerable<Ent_Pago>)> Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.PagoRepository.Obten_Paginado(RegistroPagina, NumeroPagina, TerBusqueda);
        });
    }
    public async Task<string> Actualiza_Pagar(int Pag_Id)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            var mensajeError = context.Repositories.PagoRepository.Actualiza_Pagar(Pag_Id);

            if (string.IsNullOrEmpty(mensajeError))
            {
                context.SaveChanges();
            }

            return mensajeError;
        });
    }
}