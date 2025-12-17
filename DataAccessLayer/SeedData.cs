using EntityLayer.Entities;

namespace DataAccessLayer
{
    public static class SeedData
    {
        public static readonly Guid StockType_Kagit = Guid.Parse("11111111-1111-1111-1111-111111111111");
        public static readonly Guid StockType_Zarf = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public static readonly Guid StockType_Zimba = Guid.Parse("33333333-3333-3333-3333-333333333333");
        public static readonly Guid StockType_Toner = Guid.Parse("44444444-4444-4444-4444-444444444444");
        public static readonly Guid StockType_Bant = Guid.Parse("55555555-5555-5555-5555-555555555555");
        public static readonly Guid StockType_Karton = Guid.Parse("66666666-6666-6666-6666-666666666666");
        public static readonly Guid StockType_Folyo = Guid.Parse("77777777-7777-7777-7777-777777777777");
        public static readonly Guid StockType_Forex = Guid.Parse("88888888-8888-8888-8888-888888888888");
        public static readonly Guid StockType_Bitmis = Guid.Parse("99999999-9999-9999-9999-999999999999");

        public static readonly Guid Unit_8544545 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001");
        public static readonly Guid Unit_2434536436 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000002");
        public static readonly Guid Unit_12345678906 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000003");
        public static readonly Guid Unit_744953400 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000004");
        public static readonly Guid Unit_747223200 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000005");
        public static readonly Guid Unit_748273300 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000006");
        public static readonly Guid Unit_750221650 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000007");
        public static readonly Guid Unit_750223600 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000008");
        public static readonly Guid Unit_7248273300 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000009");
        public static readonly Guid Unit_7250222500 = Guid.Parse("aaaaaaaa-0000-0000-0000-00000000000a");

        public static readonly Guid Item_716 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000001");
        public static readonly Guid Item_8003 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000002");
        public static readonly Guid Item_5151 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000003");
        public static readonly Guid Item_6016 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000004");
        public static readonly Guid Item_6013 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000005");
        public static readonly Guid Item_99 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000006");
        public static readonly Guid Item_504 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000007");
        public static readonly Guid Item_28001001 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000008");
        public static readonly Guid Item_999 = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000009");
        public static readonly Guid Item_998 = Guid.Parse("bbbbbbbb-0000-0000-0000-00000000000a");

        public static IEnumerable<StockType> StockTypes => new[]
        {
            new StockType { Id = StockType_Kagit,  Name = "Kağıt",       IsActive = true },
            new StockType { Id = StockType_Zarf,   Name = "Zarf",        IsActive = true },
            new StockType { Id = StockType_Zimba,  Name = "Zımba",       IsActive = true },
            new StockType { Id = StockType_Toner,  Name = "Toner",       IsActive = true },
            new StockType { Id = StockType_Bant,   Name = "Bant",        IsActive = true },
            new StockType { Id = StockType_Karton, Name = "Karton",      IsActive = true },
            new StockType { Id = StockType_Folyo,  Name = "Folyo",       IsActive = true },
            new StockType { Id = StockType_Forex,  Name = "Forex",       IsActive = true },
            new StockType { Id = StockType_Bitmis, Name = "Bitmiş Ürün", IsActive = true },
        };

        public static IEnumerable<StockUnit> StockUnits => new[]
        {
            new StockUnit
            {
                Id = Unit_8544545,
                Code = "8544545",
                StockTypeId = StockType_Bant,
                QuantityUnit = QuantityUnit.M3,
                BuyingCurrency = Currency.TurkishLira,
                SellingCurrency = Currency.TurkishLira,
                Description = null,
                BuyingPrice = 0.00m,
                SellingPrice = 0.00m,
                PaperWeight = null,
                IsActive = true
            },
            new StockUnit
            {
                Id = Unit_2434536436,
                Code = "2434536436",
                StockTypeId = StockType_Zimba,
                QuantityUnit = QuantityUnit.Kg,
                BuyingCurrency = Currency.TurkishLira,
                SellingCurrency = Currency.TurkishLira,
                Description = "Jnyfyv",
                BuyingPrice = 400.00m,
                SellingPrice = 700.00m,
                PaperWeight = 100,
                IsActive = true
            },
            new StockUnit
            {
                Id = Unit_12345678906,
                Code = "12345678906",
                StockTypeId = StockType_Kagit,
                QuantityUnit = QuantityUnit.Piece,
                BuyingCurrency = Currency.TurkishLira,
                SellingCurrency = Currency.TurkishLira,
                Description = "TEST",
                BuyingPrice = 0.00m,
                SellingPrice = 0.00m,
                PaperWeight = 0,
                IsActive = true
            },
            new StockUnit { Id = Unit_744953400,  Code = "744953400",  StockTypeId = StockType_Kagit, QuantityUnit = QuantityUnit.Piece, BuyingCurrency = Currency.TurkishLira, SellingCurrency = Currency.TurkishLira, Description = "744953400E",  BuyingPrice = 0m, SellingPrice = 0m, PaperWeight = 0, IsActive = true },
            new StockUnit { Id = Unit_747223200,  Code = "747223200",  StockTypeId = StockType_Kagit, QuantityUnit = QuantityUnit.Piece, BuyingCurrency = Currency.TurkishLira, SellingCurrency = Currency.TurkishLira, Description = "747223200E",  BuyingPrice = 0m, SellingPrice = 0m, PaperWeight = 0, IsActive = true },
            new StockUnit { Id = Unit_748273300,  Code = "748273300",  StockTypeId = StockType_Kagit, QuantityUnit = QuantityUnit.Piece, BuyingCurrency = Currency.TurkishLira, SellingCurrency = Currency.TurkishLira, Description = "748273300E",  BuyingPrice = 0m, SellingPrice = 0m, PaperWeight = 0, IsActive = true },
            new StockUnit { Id = Unit_750221650,  Code = "750221650",  StockTypeId = StockType_Kagit, QuantityUnit = QuantityUnit.Piece, BuyingCurrency = Currency.TurkishLira, SellingCurrency = Currency.TurkishLira, Description = "750221650E",  BuyingPrice = 0m, SellingPrice = 0m, PaperWeight = 0, IsActive = true },
            new StockUnit { Id = Unit_750223600,  Code = "750223600",  StockTypeId = StockType_Kagit, QuantityUnit = QuantityUnit.Piece, BuyingCurrency = Currency.TurkishLira, SellingCurrency = Currency.TurkishLira, Description = "750223600E",  BuyingPrice = 0m, SellingPrice = 0m, PaperWeight = 0, IsActive = true },
            new StockUnit { Id = Unit_7248273300, Code = "7248273300", StockTypeId = StockType_Kagit, QuantityUnit = QuantityUnit.Piece, BuyingCurrency = Currency.TurkishLira, SellingCurrency = Currency.TurkishLira, Description = "7248273300E", BuyingPrice = 0m, SellingPrice = 0m, PaperWeight = 0, IsActive = true },
            new StockUnit { Id = Unit_7250222500, Code = "7250222500", StockTypeId = StockType_Kagit, QuantityUnit = QuantityUnit.Piece, BuyingCurrency = Currency.TurkishLira, SellingCurrency = Currency.TurkishLira, Description = "7250222500E", BuyingPrice = 0m, SellingPrice = 0m, PaperWeight = 0, IsActive = true },
        };

        public static IEnumerable<StockItem> StockItems => new[]
        {

            new StockItem
            {
                Id = Item_716,
                StockUnitId = Unit_12345678906,
                StockClass = "RawMaterial",
                Quantity = 1232257.99m,
                CriticalQuantity = 100.00m,
                Shelf = "A-01",
                Cabinet = "D-01",
                IsActive = true
            },
            new StockItem { Id = Item_8003, StockUnitId = Unit_744953400,  StockClass = "RawMaterial", Quantity = 500.00m,        CriticalQuantity = 0.00m,   Shelf = "A-02", Cabinet = "D-01", IsActive = true },
            new StockItem { Id = Item_5151, StockUnitId = Unit_747223200,  StockClass = "RawMaterial", Quantity = 508.00m,        CriticalQuantity = 0.00m,   Shelf = "A-02", Cabinet = "D-02", IsActive = true },
            new StockItem { Id = Item_6016, StockUnitId = Unit_748273300,  StockClass = "RawMaterial", Quantity = 15.00m,         CriticalQuantity = 0.00m,   Shelf = "B-01", Cabinet = "D-02", IsActive = true },
            new StockItem { Id = Item_6013, StockUnitId = Unit_750221650,  StockClass = "RawMaterial", Quantity = 49.00m,         CriticalQuantity = 0.00m,   Shelf = "B-01", Cabinet = "D-03", IsActive = true },
            new StockItem { Id = Item_99,   StockUnitId = Unit_750223600,  StockClass = "RawMaterial", Quantity = 104891966.00m,   CriticalQuantity = 20.00m,  Shelf = "B-02", Cabinet = "D-03", IsActive = true },
            new StockItem { Id = Item_504,  StockUnitId = Unit_7248273300, StockClass = "RawMaterial", Quantity = 10000.00m,      CriticalQuantity = 1000.00m,Shelf = "C-01", Cabinet = "D-03", IsActive = true },
            new StockItem { Id = Item_28001001, StockUnitId = Unit_7250222500, StockClass = "RawMaterial", Quantity = 10.00m,     CriticalQuantity = 5.00m,   Shelf = "C-01", Cabinet = "D-04", IsActive = true },

            new StockItem { Id = Item_999, StockUnitId = Unit_8544545,     StockClass = "RawMaterial", Quantity = 124719.00m,      CriticalQuantity = 1.00m,   Shelf = "C-02", Cabinet = "D-04", IsActive = true },

            new StockItem { Id = Item_998, StockUnitId = Unit_2434536436,  StockClass = "RawMaterial", Quantity = 5451.00m,        CriticalQuantity = 0.00m,   Shelf = "D-01", Cabinet = "D-05", IsActive = true },
        };
    }
}
