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
    public int Obten_Login(string Usu_Correo, string Usu_Clave)
    {
        using var oCmd = CreateCommand("SP_Usuario_Obten_Login");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.AddWithValue("Usu_Correo", Usu_Correo);
        oCmd.Parameters.AddWithValue("Usu_Clave", Usu_Clave);

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleRow);

        oDR.Read();

        return oDR.GetInt32(oDR.GetOrdinal("CANTIDAD"));
    }
    public Ent_Usuario Obten_x_Correo(string Usu_Correo)
    {
        using var oCmd = CreateCommand("SP_Usuario_Obten_x_Correo");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.AddWithValue("Usu_Correo", Usu_Correo);

        using var oDR = oCmd.ExecuteReader(CommandBehavior.SingleRow);

        if (oDR.HasRows)
        {
            oDR.Read();

            return new Ent_Usuario
            {
                Usu_Correo = oDR.GetString(oDR.GetOrdinal("Usu_Correo")),
                Usu_Nombre = oDR.GetString(oDR.GetOrdinal("Usu_Nombre")),
                eRol = new()
                {
                    Rol_Nombre = oDR.GetString(oDR.GetOrdinal("Rol_Nombre")),
                },
                Usu_FechaHoraRegistro = oDR.GetDateTime(oDR.GetOrdinal("Usu_FechaHoraRegistro")),
                Usu_Pass = oDR.IsDBNull(oDR.GetOrdinal("Usu_Pass")) ? null : oDR.GetString(oDR.GetOrdinal("Usu_Pass")),
                Usu_Salt = oDR.IsDBNull(oDR.GetOrdinal("Usu_Salt")) ? null : oDR.GetString(oDR.GetOrdinal("Usu_Salt")),
                Usu_Token = oDR.IsDBNull(oDR.GetOrdinal("Usu_Token")) ? null : oDR.GetString(oDR.GetOrdinal("Usu_Token")),
                Usu_FechaHoraVencimientoToken = oDR.IsDBNull(oDR.GetOrdinal("Usu_FechaHoraVencimientoToken")) ? null : oDR.GetDateTime(oDR.GetOrdinal("Usu_FechaHoraVencimientoToken")),
                //Usu_TokenActualizado = oDR.IsDBNull(oDR.GetOrdinal("Usu_TokenActualizado")) ? null : oDR.GetString(oDR.GetOrdinal("Usu_TokenActualizado")),
                //Usu_FecHoraTokenActualizado = oDR.IsDBNull(oDR.GetOrdinal("Usu_FecHoraTokenActualizado")) ? null : oDR.GetDateTime(oDR.GetOrdinal("Usu_FecHoraTokenActualizado")),
                Usu_Estado = oDR.GetByte(oDR.GetOrdinal("Usu_Estado")) != 0 ? true : false,
            };
        }

        return null;
    }
    public bool Actualiza_Token(Ent_Usuario Usuario)
    {
        using var oCmd = CreateCommand("SP_Usuario_Actualiza_Token");

        oCmd.CommandType = CommandType.StoredProcedure;

        oCmd.Parameters.AddWithValue("Usu_Token", Usuario.Usu_Token);
        oCmd.Parameters.AddWithValue("Usu_FechaHoraVencimientoToken", Usuario.Usu_FechaHoraVencimientoToken);
        oCmd.Parameters.AddWithValue("Usu_Correo", Usuario.Usu_Correo);

        return oCmd.ExecuteNonQuery() > 0 ? true : false;
    }

}