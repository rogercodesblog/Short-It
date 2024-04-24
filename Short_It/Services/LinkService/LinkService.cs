using Short_It.Models;
using Short_It.Data;
using Short_It.DTOs.Link;
using CSharpVitamins;
using Microsoft.EntityFrameworkCore;

namespace Short_It.Services.LinkService
{
    public class LinkService : ILinkService
    {
        private readonly ShortItAppContext _database;
        public LinkService(ShortItAppContext database)
        {
            _database = database;
        }
        public async Task<ServiceResponse<LinkDTO>> AddLinkAsync(CreateLinkDTO createLinkDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<LinkDTO>> DeleteLinkAsync(LinkDTO linkDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<LinkDTO>> GetLinkByShortUrlAsync(string shortUrl)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerateShortUrl(string Url)
        {
            ShortGuid shortGuid = ShortGuid.NewGuid();

            for (int i = 0; i == 100; i++) 
            {

                if (!await _database.Links.AnyAsync(shortlink => shortlink.ShortLink == shortGuid))
                {
                    break;
                }
                //GUID are usually unique identifiers, if we get to the point
                //where we generate this many and we can't find
                // one that is not repeated then there's something wrong.
                if (i == 100)
                {
                    throw new OverflowException("Internal Error");
                }
                shortGuid = ShortGuid.NewGuid();
            }

            return shortGuid;
        }

    }
}
