using Model;
using Model.Entitie;

namespace Repository_Interface;

public interface ITipo_DocumentoRepository
{
    IEnumerable<Ent_Tipo_Documento> Obten();
}