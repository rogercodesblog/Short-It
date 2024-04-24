using Short_It.DTOs.Link;

namespace Short_It.Services.Link
{
    public class LinkService : ILinkService
    {
        public Task<ServiceResponse<LinkDTO>> AddLinkAsync(CreateLinkDTO createLinkDTO)
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
    }
}
