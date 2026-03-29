using AutoMapper;
using Model;
using Model.DTO;
using Model.Entitie;
namespace AlquilerWebApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //ROL

            CreateMap<Ent_Rol, DTO_Rol>()
                .ForMember(destino => destino.Rol,
                opt => opt.MapFrom(origen => origen.Rol_Nombre));
            //USUARIO

            CreateMap<Ent_Usuario, DTO_Usuario_Obten>()
                .ForMember(destino => destino.NombreUsuario,
                opt => opt.MapFrom(origen => origen.Usu_Nombre))
                .ForMember(destino => destino.Email,
                opt => opt.MapFrom(origen => origen.Usu_Correo))
                .ForMember(destino => destino.Rol,
                opt => opt.MapFrom(origen => origen.eRol.Rol_Nombre));

            CreateMap<Ent_Usuario, DTO_Usuario_Obten_x_Correo>()
                .ForMember(destino => destino.Email,
                opt => opt.MapFrom(origen => origen.Usu_Correo))
                .ForMember(destino => destino.NombreUsuario,
                opt => opt.MapFrom(origen => origen.Usu_Nombre))
                .ForMember(destino => destino.Rol,
                opt => opt.MapFrom(origen => origen.eRol.Rol_Nombre))
                .ForMember(destino => destino.FechaRegistroUsuario,
                opt => opt.MapFrom(origen => origen.Usu_FechaHoraRegistro))
                .ForMember(destino => destino.EstadoUsuario,
                opt => opt.MapFrom(origen => origen.Usu_Estado));

            CreateMap<Ent_Usuario, DTO_Usuario_Obten_Login>()
                .ForMember(destino => destino.Email,
                opt => opt.MapFrom(origen => origen.Usu_Correo))
                .ForMember(destino => destino.Clave,
                opt => opt.MapFrom(origen => origen.Usu_Clave));

            //ALOJAMIENTO

            CreateMap<Ent_Alojamiento, DTO_Alojamiento_Obten>()
                .ForMember(destino => destino.CodigoAlojamiento,
                opt => opt.MapFrom(origen => origen.Alo_Codigo))
                .ForMember(destino => destino.Descripcion,
                opt => opt.MapFrom(origen => origen.Alo_Descripcion))
                .ForMember(destino => destino.Precio,
                opt => opt.MapFrom(origen => origen.Alo_Precio))
                .ForMember(destino => destino.BanIndependiente,
                opt => opt.MapFrom(origen => origen.Alo_BanIndependiente))
                .ForMember(destino => destino.Amoblado,
                opt => opt.MapFrom(origen => origen.Alo_Amoblado))
                .ForMember(destino => destino.EstadoAlojamiento,
                opt => opt.MapFrom(origen => origen.Alo_Estado));

            //CLIENTE

            CreateMap<Ent_Cliente, DTO_Cliente_Obten_Paginado>()
                .ForMember(destino => destino.IdCliente,
                opt => opt.MapFrom(origen => origen.Cli_Id))
                .ForMember(destino => destino.NombreCliente,
                opt => opt.MapFrom(origen => origen.Cli_Nombre))
                .ForMember(destino => destino.NumDocumento,
                opt => opt.MapFrom(origen => origen.Cli_NumDocumento))
                .ForMember(destino => destino.FotoDocumento,
                opt => opt.MapFrom(origen => origen.Cli_FotoDocumento))
                .ForMember(destino => destino.Telefono,
                opt => opt.MapFrom(origen => origen.Cli_NumTelefono))
                .ForMember(destino => destino.Email,
                opt => opt.MapFrom(origen => origen.Cli_Email))
                .ForMember(destino => destino.TipoDocumento,
                opt => opt.MapFrom(origen => origen.eTipo_Documento.TipDoc_Nombre))
                .ForMember(destino => destino.EstadoCliente,
                opt => opt.MapFrom(origen => origen.Cli_Estado));

            //CONTRATO

            CreateMap<Ent_Contrato, DTO_Contrato_Obten_Paginado>()
                .ForMember(destino => destino.IdContrato,
                opt => opt.MapFrom(origen => origen.Con_Id))
                .ForMember(destino => destino.ContratoCodigo,
                opt => opt.MapFrom(origen => origen.Con_Codigo))
                .ForMember(destino => destino.DescripcionContrato,
                opt => opt.MapFrom(origen => origen.Con_Descripcion))
                .ForMember(destino => destino.FechaInicio,
                opt => opt.MapFrom(origen => origen.Con_FechaInicio))
                .ForMember(destino => destino.FechaFin,
                opt => opt.MapFrom(origen => origen.Con_FechaFin))
                .ForMember(destino => destino.PrecioAlquiler,
                opt => opt.MapFrom(origen => origen.Con_PrecioAlqDefinido))
                .ForMember(destino => destino.EstadoContrato,
                opt => opt.MapFrom(origen => origen.Con_Estado))
                .ForMember(destino => destino.NombreUsuario,
                opt => opt.MapFrom(origen => origen.eUsuario.Usu_Nombre))
                .ForMember(destino => destino.NombreCliente,
                opt => opt.MapFrom(origen => origen.eCliente.Cli_Nombre))
                .ForMember(destino => destino.NumDocumento,
                opt => opt.MapFrom(origen => origen.eCliente.Cli_NumDocumento))
                .ForMember(destino => destino.AlojamientoCodigo,
                opt => opt.MapFrom(origen => origen.eAlojamiento.Alo_Codigo))
                .ForMember(destino => destino.AdelantoC,
                opt => opt.MapFrom(origen => origen.Con_Adelanto))
                .ForMember(destino => destino.GarantiaC,
                opt => opt.MapFrom(origen => origen.Con_Garantia));

            //PAGO

            CreateMap<Ent_Pago, DTO_Pago_Obten_Paginado>()
                .ForMember(destino => destino.IdPago,
                opt => opt.MapFrom(origen => origen.Pag_Id))
                .ForMember(destino => destino.CodigoPago,
                opt => opt.MapFrom(origen => origen.Pag_Codigo))
                .ForMember(destino => destino.FechaPago,
                opt => opt.MapFrom(origen => origen.Pag_FechaPago))
                .ForMember(destino => destino.FechaVencimiento,
                opt => opt.MapFrom(origen => origen.Pag_FechaVencimieto))
                .ForMember(destino => destino.FechaPagado,
                opt => opt.MapFrom(origen => origen.Pag_FechaPagoRealizado))
                .ForMember(destino => destino.NombreUsuario,
                opt => opt.MapFrom(origen => origen.eContrato.eUsuario.Usu_Nombre))
                .ForMember(destino => destino.NombreCliente,
                opt => opt.MapFrom(origen => origen.eContrato.eCliente.Cli_Nombre))
                .ForMember(destino => destino.DNICliente,
                opt => opt.MapFrom(origen => origen.eContrato.eCliente.Cli_NumDocumento))
                .ForMember(destino => destino.CodigoHabitacion,
                opt => opt.MapFrom(origen => origen.eContrato.eAlojamiento.Alo_Codigo))
                .ForMember(destino => destino.CodigoContrato,
                opt => opt.MapFrom(origen => origen.eContrato.Con_Codigo))
                .ForMember(destino => destino.PagoCantidad,
                opt => opt.MapFrom(origen => origen.Pag_Cantidad))
                .ForMember(destino => destino.TipoPago,
                opt => opt.MapFrom(origen => origen.eTipo_Pago.Tip_Nombre))
                .ForMember(destino => destino.EstadoPago,
                opt => opt.MapFrom(origen => origen.Pag_Estado));
        }
    }
}
