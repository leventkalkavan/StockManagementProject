namespace BusinessLayer.DTOs.StockUnitDtos
{
    public class StockUnitUpdateDto
    {
        public string Code { get; set; } = "";
        public Guid StockTypeId { get; set; }
        public QuantityUnit QuantityUnit { get; set; }
        public Currency BuyingCurrency { get; set; }
        public Currency SellingCurrency { get; set; }
        public string? Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int? PaperWeight { get; set; }
    }
}
