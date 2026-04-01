using Model;
using Model.Entitie;
using Repository_Interface;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository_SqlServer;

public class Tipo_DocumentoRepository : Repository, ITipo_DocumentoRepository
{
    public Tipo_DocumentoRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public IEnumerable<Ent_Tipo_Documento> Obten()
    {
        var Lst_TipoDocumento = new List<Ent_Tipo_Documento>();

        using var oCmd = CreateCommand("SP_TipoDocumento_Obten");

        oCmd.CommandType = CommandType.StoredProcedure;

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleResult);

        while (oDR.Read())
            Lst_TipoDocumento.Add(new Ent_Tipo_Documento
            {
                TipDoc_Nombre = oDR.GetString(oDR.GetOrdinal("TipDoc_Nombre"))
            });

        return Lst_TipoDocumento;
    }
}