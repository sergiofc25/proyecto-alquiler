using Model;
using Model.Entitie;
using Repository_Interface;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository_SqlServer;

public class ContratoRepository : Repository, IContratoRepository
{
    public ContratoRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public (int, int, bool, bool, IEnumerable<Ent_Contrato>) Obten_Paginado(int RegistroPagina, int NumeroPagina, string? TerBusqueda)
    {
        var Lst_Ent_Contrato = new List<Ent_Contrato>();

        using var oCmd = CreateCommand("SP_Contrato_Obten_Paginado");

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
            Lst_Ent_Contrato.Add(new Ent_Contrato
            {
                Con_Id = oDR.GetInt32(oDR.GetOrdinal("Con_Id")),
                Con_Codigo = oDR.GetString(oDR.GetOrdinal("Con_Codigo")),
                Con_Descripcion = oDR.GetString(oDR.GetOrdinal("Con_Descripcion")),
                Con_FechaInicio = DateOnly.FromDateTime(oDR.GetDateTime(oDR.GetOrdinal("Con_FechaInicio"))),
                Con_FechaFin = oDR.IsDBNull(oDR.GetOrdinal("Con_FechaFin"))
                            ? null
                            : DateOnly.FromDateTime(oDR.GetDateTime(oDR.GetOrdinal("Con_FechaFin"))),
                Con_PrecioAlqDefinido = oDR.GetDecimal(oDR.GetOrdinal("Con_PrecioAlqDefinido")),
                Con_Estado = oDR.GetByte(oDR.GetOrdinal("Con_Estado")) != 0 ? true : false,
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
                Con_Adelanto = oDR.GetDecimal(oDR.GetOrdinal("Con_Adelanto")),
                Con_Garantia = oDR.GetDecimal(oDR.GetOrdinal("Con_Garantia"))
            });

        oDR.NextResult();

        return (Convert.ToInt32(oCmd.Parameters["TotalPagina"].Value),
            Convert.ToInt32(oCmd.Parameters["TotalRegistro"].Value),
            Convert.ToBoolean(oCmd.Parameters["TienePaginaAnterior"].Value),
            Convert.ToBoolean(oCmd.Parameters["TienePaginaProximo"].Value),
            Lst_Ent_Contrato);
    }

    public (int, string) Crea(Ent_Contrato Contrato)
    {
        using var oCmd = CreateCommand("SP_Contrato_Crea");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.AddWithValue("Con_FechaInicio", Contrato.Con_FechaInicio);
        oCmd.Parameters.AddWithValue("Con_FechaFin", Contrato.Con_FechaFin);
        oCmd.Parameters.AddWithValue("Usu_Nombre", Contrato.eUsuario.Usu_Nombre);
        oCmd.Parameters.AddWithValue("Cli_NumDocumento", Contrato.eCliente.Cli_NumDocumento);
        oCmd.Parameters.AddWithValue("Alo_Codigo", Contrato.eAlojamiento.Alo_Codigo);
        //datos si se crea nuevo cliente, pueden ser null
        oCmd.Parameters.AddWithValue("Cli_Nombre", Contrato.eCliente.Cli_Nombre);
        oCmd.Parameters.AddWithValue("Cli_NumTelefono", Contrato.eCliente.Cli_NumTelefono);
        oCmd.Parameters.AddWithValue("Cli_email", Contrato.eCliente.Cli_Email);
        oCmd.Parameters.AddWithValue("TipDoc_Nombre", Contrato.eCliente.eTipo_Documento.TipDoc_Nombre);

        oCmd.Parameters.AddWithValue("Con_Id", 0).Direction = ParameterDirection.Output;
        oCmd.Parameters.AddWithValue("MensajeError", string.Empty).Direction = ParameterDirection.Output;

        oCmd.ExecuteNonQuery();

        return ((int)oCmd.Parameters["Con_Id"].Value, oCmd.Parameters["MensajeError"].Value.ToString());
    }
    public Ent_Contrato Obten_x_Id(int Con_Id)
    {
        using var oCmd = CreateCommand("SP_Contrato_Obten_x_Id");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.AddWithValue("Con_Id", Con_Id);

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleRow);

        if (oDR.HasRows)
        {
            oDR.Read();

            return new Ent_Contrato
            {
                Con_Id = oDR.GetInt32(oDR.GetOrdinal("Con_Id")),
                Con_Codigo = oDR.GetString(oDR.GetOrdinal("Con_Codigo")),
                Con_Descripcion = oDR.GetString(oDR.GetOrdinal("Con_Descripcion")),
                Con_FechaInicio = DateOnly.FromDateTime(oDR.GetDateTime(oDR.GetOrdinal("Con_FechaInicio"))),
                Con_FechaFin = oDR.IsDBNull(oDR.GetOrdinal("Con_FechaFin"))
                            ? null
                            : DateOnly.FromDateTime(oDR.GetDateTime(oDR.GetOrdinal("Con_FechaFin"))),
                Con_PrecioAlqDefinido = oDR.GetDecimal(oDR.GetOrdinal("Con_PrecioAlqDefinido")),
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
                Con_Estado = oDR.GetByte(oDR.GetOrdinal("Con_Estado")) != 0 ? true : false,
            };
        }

        return null;
    }
}