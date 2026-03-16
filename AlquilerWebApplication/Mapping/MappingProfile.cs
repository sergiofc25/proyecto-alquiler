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
        }
    }
}
