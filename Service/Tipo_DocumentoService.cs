using Model;
using Model.Entitie;
using UnitOfWork_Interface;

namespace Service;

public interface ITipo_DocumentoService
{
    Task<IEnumerable<Ent_Tipo_Documento>> Obten();
}

public class Tipo_DocumentoService : ITipo_DocumentoService
{
    private readonly IUnitOfWork _unitOfWork;

    public Tipo_DocumentoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Ent_Tipo_Documento>> Obten()
    {
        return await Task.Run(() =>
        {
            using var context = _unitOfWork.Create();

            return context.Repositories.Tipo_DocumentoRepository.Obten();
        });
    }
}