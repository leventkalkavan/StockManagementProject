using BusinessLayer.Services.Abstractions;
using DataAccessLayer.Repositories.Abstractions;
using EntityLayer.Entities;

namespace BusinessLayer.Services
{
    public class RequestLogService : ILogService
    {
        private readonly IRepository<RequestLog> _repo;

        public RequestLogService(IRepository<RequestLog> repo)
        {
            _repo = repo;
        }

        public async Task LogAsync(RequestLog log)
        {
            log.CreatedDate = log.CreatedDate == default ? DateTime.UtcNow : log.CreatedDate;

            await _repo.AddAsync(log);
            await _repo.SaveChangesAsync();
        }
    }
}

