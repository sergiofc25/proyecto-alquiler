using Model;
using Model.Entitie;
using Repository_Interface;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository_SqlServer;

public class ClienteRepository : Repository, IClienteRepository
{
    public ClienteRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public (int, int, bool, bool, IEnumerable<Ent_Cliente>) Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda)
    {
        var Lst_Ent_Cliente = new List<Ent_Cliente>();

        using var oCmd = CreateCommand("SP_Cliente_Obten_Paginado");

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
            Lst_Ent_Cliente.Add(new Ent_Cliente
            {
                Cli_Id = oDR.GetInt32(oDR.GetOrdinal("Cli_Id")),
                Cli_Nombre = oDR.GetString(oDR.GetOrdinal("Cli_Nombre")),
                eTipo_Documento = new()
                {
                    TipDoc_Nombre = oDR.GetString(oDR.GetOrdinal("TipDoc_Nombre")),

                },
                Cli_NumDocumento = oDR.GetString(oDR.GetOrdinal("Cli_NumDocumento")),
                Cli_NumTelefono = oDR.GetString(oDR.GetOrdinal("Cli_NumTelefono")),
                Cli_Email = oDR.GetString(oDR.GetOrdinal("Cli_Email")),
                Cli_Estado = oDR.GetByte(oDR.GetOrdinal("Cli_Estado")) != 0 ? true : false,
            });

        oDR.NextResult();

        return (Convert.ToInt32(oCmd.Parameters["TotalPagina"].Value),
            Convert.ToInt32(oCmd.Parameters["TotalRegistro"].Value),
            Convert.ToBoolean(oCmd.Parameters["TienePaginaAnterior"].Value),
            Convert.ToBoolean(oCmd.Parameters["TienePaginaProximo"].Value),
            Lst_Ent_Cliente);
    }

    public Ent_Cliente Obten_x_NumDoc(string Cli_NumDocumento)
    {
        using var oCmd = CreateCommand("SP_Cliente_Obten_x_NumDoc");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.AddWithValue("Cli_NumDocumento", Cli_NumDocumento);

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleRow);

        if (oDR.HasRows)
        {
            oDR.Read();

            return new Ent_Cliente
            {
                Cli_Nombre = oDR.GetString(oDR.GetOrdinal("Cli_Nombre")),
                Cli_NumTelefono = oDR.GetString(oDR.GetOrdinal("Cli_NumTelefono")),
                Cli_NumDocumento = oDR.GetString(oDR.GetOrdinal("Cli_NumDocumento")),
                Cli_Email = oDR.GetString(oDR.GetOrdinal("Cli_Email")),
                eTipo_Documento = new()
                {
                    TipDoc_Nombre = oDR.GetString(oDR.GetOrdinal("TipDoc_Nombre")),
                },
                Cli_Estado = oDR.GetByte(oDR.GetOrdinal("Cli_Estado")) != 0 ? true : false,
            };
        }
        return null;
    }
}