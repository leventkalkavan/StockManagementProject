namespace EntityLayer.Entities
{
    public class StockType : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<StockUnit> StockUnits { get; set; }
    }
}
