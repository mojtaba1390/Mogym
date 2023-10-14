using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Interfaces.ICache
{
    public interface IRedisCacheService
    {
        Task<T?> Get<T>(string key);
        Task<T?> Set<T>(string key, T value);
        Task<T?> Set<T>(string key, T value, int absoluteExpirationMinutes);
        Task<T?> Set<T>(string key, T value, int absoluteExpirationMinutes, int slidingExpirationMinutes);

    }
}
