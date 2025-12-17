namespace BusinessLayer.DTOs.StockUnitDtos
{
    public class StockUnitDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public Guid StockTypeId { get; set; }
        public string StockTypeName { get; set; }

        public QuantityUnit QuantityUnit { get; set; }
        public string QuantityUnitText { get; set; }
        public Currency BuyingCurrency { get; set; }
        public Currency SellingCurrency { get; set; }
        public string BuyingCurrencySymbol { get; set; }
        public string SellingCurrencySymbol { get; set; }

        public string? Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int? PaperWeight { get; set; }
        public bool IsActive { get; set; }
    }
}
