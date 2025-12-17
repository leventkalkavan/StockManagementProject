using AutoMapper;
using BusinessLayer.DTOs.StockUnitDtos;

using EntityLayer.Entities;

namespace BusinessLayer.Mappings
{
    public class StockUnitProfile : Profile
    {
        public StockUnitProfile()
        {
            CreateMap<StockUnit, StockUnitDto>()
                .ForMember(d => d.StockTypeName, o => o.MapFrom(s => s.StockType.Name))
                .ForMember(d => d.QuantityUnitText, o => o.MapFrom(s => GetQuantityUnitText(s.QuantityUnit)))
                .ForMember(d => d.BuyingCurrencySymbol, o => o.MapFrom(s => GetCurrencySymbol(s.BuyingCurrency)))
                .ForMember(d => d.SellingCurrencySymbol, o => o.MapFrom(s => GetCurrencySymbol(s.SellingCurrency)));

            CreateMap<StockUnitCreateDto, StockUnit>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.StockType, o => o.Ignore())
                .ForMember(d => d.StockItems, o => o.Ignore());

            CreateMap<StockUnitUpdateDto, StockUnit>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.StockType, o => o.Ignore())
                .ForMember(d => d.StockItems, o => o.Ignore());

            CreateMap<StockUnitDto, StockUnitUpdateDto>();
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

        private static string GetCurrencySymbol(Currency currency)
        {
            return currency switch
            {
                Currency.TurkishLira => "\u20BA",
                Currency.Euro => "\u20AC",
                Currency.Dollar => "$",
                _ => ""
            };
        }
    }
}
