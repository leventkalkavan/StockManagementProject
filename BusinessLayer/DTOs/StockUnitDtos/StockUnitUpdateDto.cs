using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.StockUnitDtos
{
    public class StockUnitUpdateDto
    {
        [Required]
        [StringLength(64)]
        public string Code { get; set; } = "";

        [Required]
        public Guid StockTypeId { get; set; }

        [Required]
        public QuantityUnit QuantityUnit { get; set; }

        [Required]
        public Currency BuyingCurrency { get; set; }

        [Required]
        public Currency SellingCurrency { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal BuyingPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SellingPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int? PaperWeight { get; set; }
    }
}
