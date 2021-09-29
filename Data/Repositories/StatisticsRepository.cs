using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StatisticsRepository
    {
        private readonly ApplicationContext _context;

        public StatisticsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable> RequestsPerCountry()
        {
            return await _context.HttpHeaders
                .Where(h => h.Header == "cf-ipcountry")
                .Select(h => new
                {
                    country = h.Value,
                    count = h.HttpRequests.Count
                })
                .OrderByDescending(d => d.count)
                .ToListAsync();
        }

        public async Task<IEnumerable> RequestsPerUrl()
        {
            return await _context.RequestUrls
                .Select(u => new
                {
                    url = u.Url,
                    count = u.Requests.Count
                })
                .OrderByDescending(d => d.count)
                .ToListAsync();
        }

        public async Task<object> BandwidthPerUrl()
        {
            return await _context.RequestUrls
                .Select(
                u => new
                {
                    urlId = u.Id,
                    url = u.Url,
                    bytes = u.Requests.Sum(r => (long)r.ContentLength),
                })
                .OrderByDescending(urlSet => urlSet.bytes)
                .ToListAsync();
        }
    }
}