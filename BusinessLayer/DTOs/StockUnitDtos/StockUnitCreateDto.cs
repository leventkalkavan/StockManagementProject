namespace BusinessLayer.DTOs.StockUnitDtos
{
    public class StockUnitCreateDto
    {
        public string Code { get; set; } = "";
        public Guid StockTypeId { get; set; }
        public QuantityUnit QuantityUnit { get; set; }
        public Currency BuyingCurrency { get; set; } = Currency.TurkishLira;
        public Currency SellingCurrency { get; set; } = Currency.TurkishLira;
        public string? Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int? PaperWeight { get; set; }
    }
}
