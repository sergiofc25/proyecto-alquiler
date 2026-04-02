using Model;
using Model.Entitie;
using Repository_Interface;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository_SqlServer;

public class BoletaRepository : Repository, IBoletaRepository
{
    public BoletaRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public Ent_Boleta Obten_x_Pago(int Pag_Id)
    {
        using var oCmd = CreateCommand("SP_Boleta_Obten_x_Pago");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.AddWithValue("Pag_Id", Pag_Id);

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleRow);

        if (oDR.HasRows)
        {
            oDR.Read();

            return new Ent_Boleta
            {
                Bol_Id = oDR.GetInt32(oDR.GetOrdinal("Bol_Id")),
                Bol_Codigo = oDR.GetString(oDR.GetOrdinal("Bol_Codigo")),
                Bol_Fecha = DateOnly.FromDateTime(oDR.GetDateTime(oDR.GetOrdinal("Bol_Fecha"))),
                Bol_Descripcion = oDR.GetString(oDR.GetOrdinal("Bol_Descripcion")),
                Bol_Total = oDR.GetDecimal(oDR.GetOrdinal("Bol_Total")),
                ePago = new()
                {
                    Pag_Id = oDR.GetInt32(oDR.GetOrdinal("Pag_Id")),
                    eContrato = new()
                    {
                        eCliente = new()
                        {
                            Cli_Nombre = oDR.GetString(oDR.GetOrdinal("Cli_Nombre")),
                            Cli_NumDocumento = oDR.GetString(oDR.GetOrdinal("Cli_NumDocumento")),
                        }
                    }
                },
                Bol_Estado = oDR.GetByte(oDR.GetOrdinal("Bol_Estado")) != 0 ? true : false,
            };
        }

        return null;
    }
}