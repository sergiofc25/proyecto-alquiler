using Model;
using Model.Entitie;
using Repository_Interface;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository_SqlServer;

public class AlojamientoRepository : Repository, IAlojamientoRepository
{
    public AlojamientoRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public IEnumerable<Ent_Alojamiento> Obten()
    {
        var Lst_Alojamiento = new List<Ent_Alojamiento>();

        using var oCmd = CreateCommand("SP_Alojamiento_Obten");

        oCmd.CommandType = CommandType.StoredProcedure;

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleResult);

        while (oDR.Read())
            Lst_Alojamiento.Add(new Ent_Alojamiento
            {
                Alo_Codigo = oDR.GetString(oDR.GetOrdinal("Alo_Codigo")),
                Alo_Descripcion = oDR.GetString(oDR.GetOrdinal("Alo_Descripcion")),
                Alo_Precio = oDR.GetDecimal(oDR.GetOrdinal("Alo_Precio")),
                Alo_Garantia = oDR.GetDecimal(oDR.GetOrdinal("Alo_Garantia")),
                Alo_BanIndependiente = oDR.GetByte(oDR.GetOrdinal("Alo_BanIndependiente")) != 0 ? true : false,
                Alo_Amoblado = oDR.GetByte(oDR.GetOrdinal("Alo_Amoblado")) != 0 ? true : false,
                Alo_Estado = oDR.GetByte(oDR.GetOrdinal("Alo_Estado")) != 0 ? true : false,
            });

        return Lst_Alojamiento;
    }
}