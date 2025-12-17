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