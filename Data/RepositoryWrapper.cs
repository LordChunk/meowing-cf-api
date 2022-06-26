using Data.Repositories;

namespace Data
{
    public sealed class RepositoryWrapper
    {
        private readonly ApplicationContext _repositoryContext;

        private IHttpHeaderRepository _httpHeader;
        private IHttpRequestRepository _httpRequest;
        private IRequestUrlRepository _requestUrl;
        private StatisticsRepository _statisticsRepository;

        public IHttpHeaderRepository HttpHeader => _httpHeader ??= new HttpHeaderRepository(_repositoryContext);
        public IHttpRequestRepository HttpRequest => _httpRequest ??= new HttpRequestRepository(_repositoryContext, HttpHeader, RequestUrl);
        public IRequestUrlRepository RequestUrl => _requestUrl ??= new RequestUrlRepository(_repositoryContext);
        public StatisticsRepository Statistics => _statisticsRepository ??= new StatisticsRepository(_repositoryContext);

        public RepositoryWrapper(ApplicationContext context) => _repositoryContext = context;
    }
}