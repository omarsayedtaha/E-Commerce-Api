using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface ICachingService
    {
        Task CreateCachedResponceAsync(string Cachedkey ,object response ,TimeSpan lifetime);

        Task<string> GetCachedResponseAsync(string Cachedkey);
    }
}
