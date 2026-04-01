using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface IContratoService
{
    Task<(int, int, bool, bool, IEnumerable<Ent_Contrato>)> Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda);
    Task<(int, string)> Crea(Ent_Contrato Contrato);
    Task<Ent_Contrato> Obten_x_Id(int Con_Id);
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
    public async Task<(int, string)> Crea(Ent_Contrato Contrato)
    {
        using var context = _unitOfWork.Create();

        var (Con_Id, MensajeError) = context.Repositories.ContratoRepository.Crea(Contrato);

        if (Con_Id > 0 && MensajeError == string.Empty)
        {
            context.SaveChanges();
        }

        return (Con_Id, MensajeError);
    }
    public async Task<Ent_Contrato> Obten_x_Id(int Con_Id)
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.ContratoRepository.Obten_x_Id(Con_Id);
        });
    }
}