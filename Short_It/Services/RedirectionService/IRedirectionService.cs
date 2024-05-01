using Short_It.DTOs.Link;

namespace Short_It.Services.RedirectionService
{
    public interface IRedirectionService
    {
        Task<ServiceResponse<string>> GetLinkToRedirectByShortUrlAsync(string shortUrl);
    }
}
