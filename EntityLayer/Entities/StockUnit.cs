using EntityLayer.Entities;

public enum QuantityUnit
{
    Piece,
    Kg,
    Meter,
    Liter,
    M3
}

public enum Currency
{
    TurkishLira,
    Euro,
    Dollar
}

public class StockUnit : BaseEntity
{
    public string Code { get; set; }
    public Guid StockTypeId { get; set; }
    public StockType StockType { get; set; }

    public ICollection<StockItem> StockItems { get; set; }

    public QuantityUnit QuantityUnit { get; set; }
    public Currency BuyingCurrency { get; set; } = Currency.TurkishLira;
    public Currency SellingCurrency { get; set; } = Currency.TurkishLira;

    public string? Description { get; set; }
    public decimal BuyingPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int? PaperWeight { get; set; }
}
