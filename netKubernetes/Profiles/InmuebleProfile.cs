using AutoMapper;
using netKubernetes.Dto.InmuebleDto;
using netKubernetes.Models;

namespace netKubernetes.Profiles
{
    public class InmuebleProfile : Profile
    {
        public InmuebleProfile()
        {
             CreateMap<Inmueble, InmuebleResponseDto>();
             CreateMap<InmuebleRequestDto, Inmueble>();
        }

    }
}
