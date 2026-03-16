using Model;
using Model.Entitie;
using Repository_Interface;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository_SqlServer;

public class UsuarioRepository : Repository, IUsuarioRepository
{
    public UsuarioRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public IEnumerable<Ent_Usuario> Obten()
    {
        var Lst_Usuario = new List<Ent_Usuario>();

        using var oCmd = CreateCommand("SP_Usuario_Obten");

        oCmd.CommandType = CommandType.StoredProcedure;

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleResult);

        while (oDR.Read())
            Lst_Usuario.Add(new Ent_Usuario
            {
                Usu_Nombre = oDR.GetString(oDR.GetOrdinal("Usu_Nombre")),
                Usu_Correo = oDR.GetString(oDR.GetOrdinal("Usu_Correo")),
                eRol = new Ent_Rol
                {
                    Rol_Nombre = oDR.GetString(oDR.GetOrdinal("Rol_Nombre"))
                }
            });

        return Lst_Usuario;
    }
}