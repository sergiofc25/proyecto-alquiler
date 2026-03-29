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
        }
    }
}
