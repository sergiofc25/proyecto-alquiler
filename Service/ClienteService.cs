using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IClienteService
{
    Task<(int, int, bool, bool, IEnumerable<Ent_Cliente>)> Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda);
    Task<Ent_Cliente> Obten_x_NumDoc(string Cli_NumDocumento);

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
    public async Task<Ent_Cliente> Obten_x_NumDoc(string Cli_NumDocumento)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.ClienteRepository.Obten_x_NumDoc(Cli_NumDocumento);
        });
    }
}