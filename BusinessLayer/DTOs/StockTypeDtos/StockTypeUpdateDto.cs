using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.StockTypeDtos
{
    public class StockTypeUpdateDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}
