using Model;
using Model.Entitie;
using Repository_Interface;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository_SqlServer;

public class PagoRepository : Repository, IPagoRepository
{
    public PagoRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public (int, int, bool, bool, IEnumerable<Ent_Pago>) Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda)
    {
        var Lst_Ent_Pago = new List<Ent_Pago>();

        using var oCmd = CreateCommand("SP_Pago_Obten_Paginado");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.AddWithValue("RegistroPagina", RegistroPagina);
        oCmd.Parameters.AddWithValue("NumeroPagina", NumeroPagina);
        oCmd.Parameters.AddWithValue("TerBusqueda", TerBusqueda != null ? (object)TerBusqueda : DBNull.Value);
        oCmd.Parameters.AddWithValue("TotalPagina", 0).Direction = ParameterDirection.Output;
        oCmd.Parameters.AddWithValue("TotalRegistro", 0).Direction = ParameterDirection.Output;
        oCmd.Parameters.AddWithValue("TienePaginaAnterior", 0).Direction = ParameterDirection.Output;
        oCmd.Parameters.AddWithValue("TienePaginaProximo", 0).Direction = ParameterDirection.Output;

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleResult);

        while (oDR.Read())
            Lst_Ent_Pago.Add(new Ent_Pago
            {
                Pag_Id = oDR.GetInt32(oDR.GetOrdinal("Pag_Id")),
                Pag_Codigo = oDR.IsDBNull(oDR.GetOrdinal("Pag_Codigo"))
                            ? (string?)null
                            : oDR.GetString(oDR.GetOrdinal("Pag_Codigo")),
                Pag_FechaPago = oDR.IsDBNull(oDR.GetOrdinal("Pag_FechaPago"))
                            ? null
                            : DateOnly.FromDateTime(oDR.GetDateTime(oDR.GetOrdinal("Pag_FechaPago"))),

                Pag_FechaVencimieto = oDR.IsDBNull(oDR.GetOrdinal("Pag_FechaVencimieto"))
                                  ? null
                                  : DateOnly.FromDateTime(oDR.GetDateTime(oDR.GetOrdinal("Pag_FechaVencimieto"))),

                Pag_FechaPagoRealizado = oDR.IsDBNull(oDR.GetOrdinal("Pag_FechaPagoRealizado"))
                                     ? null
                                     : DateOnly.FromDateTime(oDR.GetDateTime(oDR.GetOrdinal("Pag_FechaPagoRealizado"))),
                eContrato = new()
                {
                    eUsuario = new()
                    {
                        Usu_Nombre = oDR.GetString(oDR.GetOrdinal("Usu_Nombre")),
                    },
                    eCliente = new()
                    {
                        Cli_Nombre = oDR.GetString(oDR.GetOrdinal("Cli_Nombre")),
                        Cli_NumDocumento = oDR.GetString(oDR.GetOrdinal("Cli_NumDocumento")),
                    },
                    eAlojamiento = new()
                    {
                        Alo_Codigo = oDR.GetString(oDR.GetOrdinal("Alo_Codigo")),
                    },
                    Con_Codigo = oDR.GetString(oDR.GetOrdinal("Con_Codigo")),
                },
                Pag_Cantidad = oDR.GetDecimal(oDR.GetOrdinal("Pag_Cantidad")),
                eTipo_Pago = new()
                {
                    Tip_Nombre = oDR.GetString(oDR.GetOrdinal("Tip_Nombre")),
                },
                Pag_Estado = oDR.GetByte(oDR.GetOrdinal("Pag_Estado")) != 0 ? true : false,
            });

        oDR.NextResult();

        return (Convert.ToInt32(oCmd.Parameters["TotalPagina"].Value),
            Convert.ToInt32(oCmd.Parameters["TotalRegistro"].Value),
            Convert.ToBoolean(oCmd.Parameters["TienePaginaAnterior"].Value),
            Convert.ToBoolean(oCmd.Parameters["TienePaginaProximo"].Value),
            Lst_Ent_Pago);
    }

    public string Actualiza_Pagar(int Pag_Id)
    {
        using var oCmd = CreateCommand("SP_Pago_Actualiza_Pagar");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.Add("@Pag_Id", SqlDbType.Int).Value = Pag_Id;

        var pMensaje = new SqlParameter("@MensajeError", SqlDbType.VarChar, 500)
        {
            Direction = ParameterDirection.Output
        };

        oCmd.Parameters.Add(pMensaje);

        oCmd.ExecuteNonQuery();

        return pMensaje.Value?.ToString();
    }
}