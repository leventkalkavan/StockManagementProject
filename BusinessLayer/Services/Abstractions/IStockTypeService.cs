using BusinessLayer.DTOs;
using BusinessLayer.DTOs.StockTypeDtos;

namespace BusinessLayer.Services.Abstractions
{
    public interface IStockTypeService
    {
        Task<PagedResult<StockTypeDto>> GetPagedAsync(int page, int pageSize, string? search, bool onlyActive = true);
        Task<List<StockTypeDto>> GetAllAsync(bool onlyActive = true);
        Task<StockTypeDto?> GetByIdAsync(Guid id);

        Task<StockTypeDto> CreateAsync(StockTypeCreateDto dto);
        Task UpdateAsync(Guid id, StockTypeUpdateDto dto);
        Task DeactivateAsync(Guid id);
    }
}
