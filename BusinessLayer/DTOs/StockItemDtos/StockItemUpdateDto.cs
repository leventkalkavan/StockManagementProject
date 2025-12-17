namespace BusinessLayer.DTOs.StockItemDtos
{
    public sealed class StockItemUpdateDto
    {
        public Guid StockUnitId { get; set; }
        public string StockClass { get; set; }
        public decimal Quantity { get; set; }
        public decimal CriticalQuantity { get; set; }
        public string? Shelf { get; set; }
        public string? Cabinet { get; set; }
    }
}
