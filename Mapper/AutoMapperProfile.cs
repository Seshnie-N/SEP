using AutoMapper;
using SEP.Models.DomainModels;
using SEP.Models.FrontEndModels;

namespace SEP.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudentApplicationDetailsDto, Post>().ReverseMap();

        }
    }
}
