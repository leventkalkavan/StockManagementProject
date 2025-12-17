using AutoMapper;
using BusinessLayer.DTOs.StockTypeDtos;
using BusinessLayer.Services.Abstractions;
using DataAccessLayer.Repositories.Abstractions;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class StockTypeService : IStockTypeService
    {
        private readonly IRepository<StockType> _stockTypeRepo;
        private readonly IMapper _mapper;

        public StockTypeService(IRepository<StockType> stockTypeRepo, IMapper mapper)
        {
            _stockTypeRepo = stockTypeRepo;
            _mapper = mapper;
        }

        public async Task<List<StockTypeDto>> GetAllAsync(bool onlyActive = true)
        {
            var q = _stockTypeRepo.Query();
            if (onlyActive) q = q.Where(x => x.IsActive);

            var entities = await q
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
            return _mapper.Map<List<StockTypeDto>>(entities);
        }

        public async Task<StockTypeDto?> GetByIdAsync(Guid id)
        {
            var entity = await _stockTypeRepo.Query()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : _mapper.Map<StockTypeDto>(entity);
        }

        public async Task<StockTypeDto> CreateAsync(StockTypeCreateDto dto)
        {
            var name = (dto?.Name ?? "").Trim();

            var exists = await _stockTypeRepo.Query().AnyAsync(x => x.Name == name);
            if (exists)
                throw new InvalidOperationException("Stock type name must be unique.");

            var entity = _mapper.Map<StockType>(dto);

            entity.Id = Guid.NewGuid();
            entity.Name = name;
            entity.IsActive = true;
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;

            await _stockTypeRepo.AddAsync(entity);
            await _stockTypeRepo.SaveChangesAsync();

            return _mapper.Map<StockTypeDto>(entity);
        }

        public async Task UpdateAsync(Guid id, StockTypeUpdateDto dto)
        {
            var name = (dto?.Name ?? "").Trim();

            var entity = await _stockTypeRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("StockType not found.");

            var exists = await _stockTypeRepo.Query().AnyAsync(x => x.Id != id && x.Name == name);
            if (exists)
                throw new InvalidOperationException("Stock type name must be unique.");

            _mapper.Map(dto, entity);

            entity.Name = name;
            entity.UpdatedDate = DateTime.UtcNow;

            _stockTypeRepo.Update(entity);
            await _stockTypeRepo.SaveChangesAsync();
        }

        public async Task DeactivateAsync(Guid id)
        {
            var entity = await _stockTypeRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("StockType not found.");

            entity.IsActive = false;
            entity.UpdatedDate = DateTime.UtcNow;

            _stockTypeRepo.Update(entity);
            await _stockTypeRepo.SaveChangesAsync();
        }
    }
}
