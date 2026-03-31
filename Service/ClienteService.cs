using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IClienteService
{
    Task<(int, int, bool, bool, IEnumerable<Ent_Cliente>)> Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda);
}

public class ClienteService : IClienteService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClienteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(int, int, bool, bool, IEnumerable<Ent_Cliente>)> Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.ClienteRepository.Obten_Paginado(RegistroPagina, NumeroPagina, TerBusqueda);
        });
    }
}