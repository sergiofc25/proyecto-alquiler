using Repository_Interface;

namespace UnitOfWork_Interface;

public interface IUnitOfWorkRepository
{
    IRolRepository RolRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
    IAlojamientoRepository AlojamientoRepository { get; }
    IClienteRepository ClienteRepository { get; }

}