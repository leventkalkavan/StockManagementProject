using BusinessLayer.DTOs;
using BusinessLayer.DTOs.StockItemDtos;

namespace BusinessLayer.Services.Abstractions
{
    public interface IStockItemService
    {
        Task<List<StockItemDto>> GetAllAsync(bool onlyActive = true);
        Task<StockItemDto?> GetByIdAsync(Guid id);

        Task<StockItemDto> CreateAsync(StockItemCreateDto dto);
        Task UpdateAsync(Guid id, StockItemUpdateDto dto);
        Task DeactivateAsync(Guid id);
    }
}