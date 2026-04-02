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
            ContratoRepository = new ContratoRepository(context, transaction);
            PagoRepository = new PagoRepository(context, transaction);
            BoletaRepository = new BoletaRepository(context, transaction);
        }

        public IRolRepository RolRepository { get; }
        public IUsuarioRepository UsuarioRepository { get; }
        public IAlojamientoRepository AlojamientoRepository { get; }
        public IClienteRepository ClienteRepository { get; }
        public IContratoRepository ContratoRepository { get; }
        public IPagoRepository PagoRepository { get; }
        public ITipo_DocumentoRepository Tipo_DocumentoRepository { get; }
        public IBoletaRepository BoletaRepository { get; }
    }
}