using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IContratoService
{
    Task<(int, int, bool, bool, IEnumerable<Ent_Contrato>)> Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda);
}

public class ContratoService : IContratoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContratoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(int, int, bool, bool, IEnumerable<Ent_Contrato>)> Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.ContratoRepository.Obten_Paginado(RegistroPagina, NumeroPagina, TerBusqueda);
        });
    }
}