using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.StockItemDtos
{
    public sealed class StockItemUpdateDto
    {
        [Required]
        public Guid StockUnitId { get; set; }

        [Required]
        [StringLength(100)]
        public string StockClass { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CriticalQuantity { get; set; }

        [StringLength(50)]
        public string? Shelf { get; set; }

        [StringLength(50)]
        public string? Cabinet { get; set; }
    }
}
