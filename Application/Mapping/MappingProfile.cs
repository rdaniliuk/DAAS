using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAccessRequestDto, AccessRequest>();
            CreateMap<AccessRequest, AccessRequestDto>();
            CreateMap<Decision, DecisionDto>();
        }
    }
}
