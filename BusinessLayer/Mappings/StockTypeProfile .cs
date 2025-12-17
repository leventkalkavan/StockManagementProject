using AutoMapper;
using BusinessLayer.DTOs.StockTypeDtos;
using EntityLayer.Entities;

namespace BusinessLayer.Mappings
{
    public class StockTypeProfile : Profile
    {
        public StockTypeProfile()
        {
            CreateMap<StockType, StockTypeDto>();

            CreateMap<StockTypeCreateDto, StockType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());

            CreateMap<StockTypeUpdateDto, StockType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());

            CreateMap<StockTypeDto, StockTypeUpdateDto>();
        }
    }
}
