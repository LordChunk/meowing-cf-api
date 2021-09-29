using System.Collections;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StatisticsController : ControllerBase
    {
        private readonly RepositoryWrapper _repositoryWrapper;

        public StatisticsController(RepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> RequestsPerCountry() => Ok(await _repositoryWrapper.Statistics.RequestsPerCountry());

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> RequestsPerUrl() => Ok(await _repositoryWrapper.Statistics.RequestsPerUrl());

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> BandwidthPerUrl() => Ok(await _repositoryWrapper.Statistics.BandwidthPerUrl());
    }
}
