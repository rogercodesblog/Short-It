
using Microsoft.EntityFrameworkCore;
using Short_It.Data;
using Short_It.DTOs.Link;

namespace Short_It.Services.RedirectionService
{
    public class RedirectionService : IRedirectionService
    {

        private readonly ShortItAppContext _database;
        public RedirectionService(ShortItAppContext database)
        {
            _database = database;
        }

        public async Task<ServiceResponse<string>> GetLinkToRedirectByShortUrlAsync(string shortUrl)
        {

            ServiceResponse<string> _response = new ServiceResponse<string>();

            try
            {
                var _linkToReturn = await _database.Links.FirstOrDefaultAsync(shortLink => shortLink.ShortLink == shortUrl);

                if (_linkToReturn == null)
                {
                    _response.Success = false;
                    _response.Data = null;
                    _response.Message = "The requested link does not exists";
                    return _response;
                }

                _response.Success = true;
                _response.Data = _linkToReturn.FullLink;
                _response.Message = "The link was fetch successfully";

                _linkToReturn.TimesVisited++;
                await _database.SaveChangesAsync();

            }
            catch (Exception)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "There was an internal server error, try again, if the error persist please do comunicate with our it staff";
            }

            return _response;

        }

    }
}
