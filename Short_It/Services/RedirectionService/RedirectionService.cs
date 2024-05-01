
using Short_It.Data;

namespace Short_It.Services.RedirectionService
{
    public class RedirectionService : IRedirectionService
    {

        private readonly ShortItAppContext _database;
        public RedirectionService(ShortItAppContext database)
        {
            _database = database;
        }

        public Task<ServiceResponse<string>> GetLinkToRedirectByShortUrlAsync(string shortUrl)
        {
            throw new NotImplementedException();
        }

    }
}
