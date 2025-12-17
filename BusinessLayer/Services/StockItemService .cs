using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.StockItemDtos;
using BusinessLayer.Services.Abstractions;
using DataAccessLayer.Repositories.Abstractions;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class StockItemService : IStockItemService
    {
        private readonly IRepository<StockItem>  _stockItemRepo;
        private readonly IRepository<StockUnit> _stockUnitRepo;
        private readonly IMapper _mapper;

        public StockItemService(
            IRepository<StockItem> stockItemRepo,
            IRepository<StockUnit> stockUnitRepo,
            IMapper mapper)
        {
             _stockItemRepo = stockItemRepo;
            _stockUnitRepo = stockUnitRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<StockItemDto>> GetPagedAsync(int page, int pageSize, string? search, bool onlyActive = true)
        {
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            IQueryable<StockItem> q = _stockItemRepo.Query()
                .Include(x => x.StockUnit)
                .ThenInclude(u => u.StockType);

            if (onlyActive)
                q = q.Where(x => x.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                var hasGuid = Guid.TryParse(term, out var searchId);
                q = q.Where(x =>
                    (hasGuid && x.Id == searchId) ||
                    EF.Functions.Like(x.StockUnit.Code, $"%{term}%") ||
                    EF.Functions.Like(x.StockUnit.Description ?? string.Empty, $"%{term}%") ||
                    EF.Functions.Like(x.StockUnit.StockType.Name, $"%{term}%") ||
                    EF.Functions.Like(x.Shelf ?? string.Empty, $"%{term}%") ||
                    EF.Functions.Like(x.Cabinet ?? string.Empty, $"%{term}%"));
            }

            var totalCount = await q.CountAsync();

            var skip = (page - 1) * pageSize;
            if (skip >= totalCount && totalCount > 0)
            {
                page = (int)Math.Ceiling(totalCount / (double)pageSize);
                skip = (page - 1) * pageSize;
            }

            var items = await q
                .OrderByDescending(x => x.CreatedDate)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<StockItemDto>
            {
                Items = _mapper.Map<List<StockItemDto>>(items),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                SearchTerm = search?.Trim()
            };
        }

        public async Task<List<StockItemDto>> GetAllAsync(bool onlyActive = true)
        {
            IQueryable<StockItem> q =  _stockItemRepo.Query()
                .Include(x => x.StockUnit)
                .ThenInclude(u => u.StockType);

            if (onlyActive)
                q = q.Where(x => x.IsActive);

            var entities = await q
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return _mapper.Map<List<StockItemDto>>(entities);
        }

        public async Task<StockItemDto?> GetByIdAsync(Guid id)
        {
            var entity = await  _stockItemRepo.Query()
                .Include(x => x.StockUnit)
                .ThenInclude(u => u.StockType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : _mapper.Map<StockItemDto>(entity);
        }

        public async Task<StockItemDto> CreateAsync(StockItemCreateDto dto)
        {
            var unitExists = await _stockUnitRepo.Query().AnyAsync(u => u.Id == dto.StockUnitId);
            if (!unitExists)
                throw new KeyNotFoundException("StockUnit not found.");

            var duplicate = await _stockItemRepo.Query()
                .AnyAsync(x => x.StockUnitId == dto.StockUnitId);
            if (duplicate)
                throw new InvalidOperationException("Stock item already exists for this unit.");

            var entity = _mapper.Map<StockItem>(dto);

            entity.Id = Guid.NewGuid();
            entity.IsActive = true;
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;

            await  _stockItemRepo.AddAsync(entity);
            await  _stockItemRepo.SaveChangesAsync();

            var saved = await  _stockItemRepo.Query()
                .Include(x => x.StockUnit)
                .ThenInclude(u => u.StockType)
                .FirstAsync(x => x.Id == entity.Id);

            return _mapper.Map<StockItemDto>(saved);
        }

        public async Task UpdateAsync(Guid id, StockItemUpdateDto dto)
        {
            var entity = await  _stockItemRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("StockItem not found.");

            var unitExists = await _stockUnitRepo.Query().AnyAsync(u => u.Id == dto.StockUnitId);
            if (!unitExists)
                throw new KeyNotFoundException("StockUnit not found.");

            var duplicate = await _stockItemRepo.Query()
                .AnyAsync(x => x.StockUnitId == dto.StockUnitId && x.Id != id);
            if (duplicate)
                throw new InvalidOperationException("Stock item already exists for this unit.");

            _mapper.Map(dto, entity);

            entity.UpdatedDate = DateTime.UtcNow;

             _stockItemRepo.Update(entity);
            await  _stockItemRepo.SaveChangesAsync();
        }

        public async Task DeactivateAsync(Guid id)
        {
            var entity = await  _stockItemRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("StockItem not found.");

            entity.IsActive = false;
            entity.UpdatedDate = DateTime.UtcNow;

             _stockItemRepo.Update(entity);
            await  _stockItemRepo.SaveChangesAsync();
        }
    }
}
