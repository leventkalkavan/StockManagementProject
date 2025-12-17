using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.StockItemDtos;
using EntityLayer.Entities;

namespace BusinessLayer.Mappings
{
    public class StockItemProfile : Profile
    {
        public StockItemProfile()
        {
            CreateMap<StockItem, StockItemDto>()
                .ForMember(d => d.StockUnitCode,
                    o => o.MapFrom(s => s.StockUnit.Code))
                .ForMember(d => d.StockUnitDescription,
                    o => o.MapFrom(s => s.StockUnit.Description))
                .ForMember(d => d.StockTypeName,
                    o => o.MapFrom(s => s.StockUnit.StockType.Name))
                .ForMember(d => d.QuantityText,
                    o => o.MapFrom(s =>
                        $"{s.Quantity:N2} {GetQuantityUnitText(s.StockUnit.QuantityUnit)}"))
                .ForMember(d => d.CriticalQuantityText,
                    o => o.MapFrom(s =>
                        $"{s.CriticalQuantity:N2} {GetQuantityUnitText(s.StockUnit.QuantityUnit)}"));

            CreateMap<StockItemCreateDto, StockItem>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.StockUnit, o => o.Ignore());

            CreateMap<StockItemUpdateDto, StockItem>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.StockUnit, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.Ignore());

            CreateMap<StockItemDto, StockItemUpdateDto>();
        }

        private static string GetQuantityUnitText(QuantityUnit unit)
        {
            return unit switch
            {
                QuantityUnit.Piece => "Adet",
                QuantityUnit.Kg => "Gram",
                QuantityUnit.Meter => "Metre",
                QuantityUnit.Liter => "Litre",
                QuantityUnit.M3 => "m3",
                _ => unit.ToString()
            };
        }
    }
}
