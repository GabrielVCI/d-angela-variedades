using AutoMapper;
using d_angela_variedades.Entidades;
using d_angela_variedades.Helper;

namespace d_angela_variedades.Data
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile() 
        {
            CreateMap<Empresas, EmpresasDTO>().ReverseMap();
        }
    }
}
