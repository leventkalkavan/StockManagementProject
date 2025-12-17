using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.StockTypeDtos
{
    public class StockTypeCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}
