using Model;
using Model.Entitie;
using Repository_Interface;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository_SqlServer;

public class RolRepository : Repository, IRolRepository
{
    public RolRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public IEnumerable<Ent_Rol> Obten()
    {
        var Lst_Rol = new List<Ent_Rol>();

        using var oCmd = CreateCommand("SP_Rol_Obten");

        oCmd.CommandType = CommandType.StoredProcedure;

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleResult);

        while (oDR.Read())
            Lst_Rol.Add(new Ent_Rol
            {
                Rol_Nombre = oDR.GetString(oDR.GetOrdinal("Rol_Nombre"))
            });

        return Lst_Rol;
    }
}