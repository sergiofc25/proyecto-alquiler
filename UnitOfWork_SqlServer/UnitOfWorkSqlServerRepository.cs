using Repository_Interface;
using Repository_SqlServer;
using Microsoft.Data.SqlClient;
using UnitOfWork_Interface;

namespace UnitOfWork_SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            RolRepository = new RolRepository(context, transaction);
            UsuarioRepository = new UsuarioRepository(context, transaction);
            AlojamientoRepository = new AlojamientoRepository(context, transaction);
            ClienteRepository = new ClienteRepository(context, transaction);
        }

        public IRolRepository RolRepository { get; }
        public IUsuarioRepository UsuarioRepository { get; }
        public IAlojamientoRepository AlojamientoRepository { get; }
        public IClienteRepository ClienteRepository { get; }
    }
}