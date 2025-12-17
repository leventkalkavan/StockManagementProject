namespace EntityLayer.Entities
{
    public class StockItem : BaseEntity
    {
        public Guid StockUnitId { get; set; }
        public StockUnit StockUnit { get; set; }

        public string StockClass { get; set; }
        public decimal Quantity { get; set; }

        public string Shelf { get; set; }

        public string Cabinet { get; set; }

        public decimal CriticalQuantity { get; set; }
    }
}
