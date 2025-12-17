using BusinessLayer.DTOs;
using BusinessLayer.DTOs.StockUnitDtos;

namespace BusinessLayer.Services.Abstractions
{
    public interface IStockUnitService
    {
        Task<PagedResult<StockUnitDto>> GetPagedAsync(int page, int pageSize, string? search, bool onlyActive = true);
        Task<List<StockUnitDto>> GetAllAsync(bool onlyActive = true);
        Task<StockUnitDto?> GetByIdAsync(Guid id);

        Task<StockUnitDto> CreateAsync(StockUnitCreateDto dto);
        Task UpdateAsync(Guid id, StockUnitUpdateDto dto);
        Task DeactivateAsync(Guid id);
    }
}
