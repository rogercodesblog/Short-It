using Microsoft.AspNetCore.SignalR.Protocol;
using Short_It.DTOs.Link;

namespace Short_It.Services.Link
{
    public interface ILinkService
    {
        Task<ServiceResponse<LinkDTO>> AddLinkAsync(CreateLinkDTO createLinkDTO);
        Task<ServiceResponse<LinkDTO>> GetLinkByShortUrlAsync(string shortUrl);
        Task<ServiceResponse<LinkDTO>> DeleteLinkAsync(LinkDTO linkDTO);
    }
}