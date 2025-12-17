using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.StockUnitDtos;
using BusinessLayer.Services.Abstractions;
using DataAccessLayer.Repositories.Abstractions;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class StockUnitService : IStockUnitService
    {
        private readonly IRepository<StockUnit> _stockUnitRepo;
        private readonly IMapper _mapper;

        public StockUnitService(IRepository<StockUnit> stockUnitRepo, IMapper mapper)
        {
            _stockUnitRepo = stockUnitRepo;
            _mapper = mapper;
        }

        public async Task<PagedResult<StockUnitDto>> GetPagedAsync(int page, int pageSize, string? search, bool onlyActive = true)
        {
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            IQueryable<StockUnit> q = _stockUnitRepo.Query()
                .Include(x => x.StockType);

            if (onlyActive)
                q = q.Where(x => x.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                var hasGuid = Guid.TryParse(term, out var searchId);
                q = q.Where(x =>
                    (hasGuid && x.Id == searchId) ||
                    EF.Functions.Like(x.Code, $"%{term}%") ||
                    EF.Functions.Like(x.Description ?? string.Empty, $"%{term}%") ||
                    EF.Functions.Like(x.StockType.Name, $"%{term}%"));
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

            return new PagedResult<StockUnitDto>
            {
                Items = _mapper.Map<List<StockUnitDto>>(items),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                SearchTerm = search?.Trim()
            };
        }

        public async Task<List<StockUnitDto>> GetAllAsync(bool onlyActive = true)
        {
            IQueryable<StockUnit> q = _stockUnitRepo.Query()
                .Include(x => x.StockType);

            if (onlyActive)
                q = q.Where(x => x.IsActive);

            var entities = await q
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return _mapper.Map<List<StockUnitDto>>(entities);
        }

        public async Task<StockUnitDto?> GetByIdAsync(Guid id)
        {
            var entity = await _stockUnitRepo.Query()
                .Include(x => x.StockType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : _mapper.Map<StockUnitDto>(entity);
        }

        public async Task<StockUnitDto> CreateAsync(StockUnitCreateDto dto)
        {
            var code = (dto?.Code ?? "").Trim();

            var exists = await _stockUnitRepo.Query().AnyAsync(x => x.Code == code);
            if (exists)
                throw new InvalidOperationException("Unit code must be unique.");

            var entity = _mapper.Map<StockUnit>(dto);

            entity.Id = Guid.NewGuid();
            entity.Code = code;
            entity.IsActive = true;
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;

            await _stockUnitRepo.AddAsync(entity);
            await _stockUnitRepo.SaveChangesAsync();

            var saved = await _stockUnitRepo.Query()
                .Include(x => x.StockType)
                .FirstAsync(x => x.Id == entity.Id);

            return _mapper.Map<StockUnitDto>(saved);
        }

        public async Task UpdateAsync(Guid id, StockUnitUpdateDto dto)
        {
            var code = (dto?.Code ?? "").Trim();

            var entity = await _stockUnitRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("StockUnit not found.");

            var exists = await _stockUnitRepo.Query().AnyAsync(x => x.Id != id && x.Code == code);
            if (exists)
                throw new InvalidOperationException("Unit code must be unique.");

            _mapper.Map(dto, entity);

            entity.Code = code;
            entity.UpdatedDate = DateTime.UtcNow;

            _stockUnitRepo.Update(entity);
            await _stockUnitRepo.SaveChangesAsync();
        }

        public async Task DeactivateAsync(Guid id)
        {
            var entity = await _stockUnitRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("StockUnit not found.");

            entity.IsActive = false;
            entity.UpdatedDate = DateTime.UtcNow;

            _stockUnitRepo.Update(entity);
            await _stockUnitRepo.SaveChangesAsync();
        }
    }
}
