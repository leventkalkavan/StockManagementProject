using EntityLayer.Entities;

namespace BusinessLayer.Services.Abstractions
{
    public interface ILogService
    {
        Task LogAsync(RequestLog log);
    }
}

