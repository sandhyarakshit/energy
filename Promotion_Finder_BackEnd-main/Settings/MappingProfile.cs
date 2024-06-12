using AutoMapper;
using SubwayPromotion.Models;
namespace SubwayPromotion.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PromotionDTO, Promotion>()
            .ForMember(dest => dest.PromotionID, opt => opt.MapFrom(src => src.Id));
            CreateMap<Promotion, PromotionDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PromotionID));
            CreateMap<LocationDTO, Location>();
            CreateMap<Location, LocationDTO>();
            CreateMap<TermDTO, Term>();
            CreateMap<Term, TermDTO>();
        }
    }
}