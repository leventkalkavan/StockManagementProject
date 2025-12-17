namespace BusinessLayer.DTOs
{
    public sealed class StockItemDto
    {
        public Guid Id { get; set; }
        public Guid StockUnitId { get; set; }
        public string StockUnitCode { get; set; }
        public string? StockUnitDescription { get; set; }
        public string StockTypeName { get; set; }
        public string StockClass { get; set; }
        public string QuantityText { get; set; }
        public string CriticalQuantityText { get; set; }
        public decimal Quantity { get; set; }
        public decimal CriticalQuantity { get; set; }
        public string? Shelf { get; set; }
        public string? Cabinet { get; set; }
        public bool IsActive { get; set; }
    }

}