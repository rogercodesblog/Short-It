using Short_It.Models;
using Short_It.Data;
using Short_It.DTOs.Link;
using CSharpVitamins;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

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

            ServiceResponse<LinkDTO> _response = new ServiceResponse<LinkDTO>();

            try
            {

                var linkTitle = await GetLinkTitle(createLinkDTO.FullLink);
                var generatedShortLink = await GenerateShortLink(createLinkDTO.FullLink);

                Link newLink = new Link()
                {
                    DateCreated = DateTime.Now,
                    FullLink = createLinkDTO.FullLink,
                    ShortLink =  generatedShortLink,
                    LinkTitle = !string.IsNullOrWhiteSpace(linkTitle) ? linkTitle : $"Generated Link {generatedShortLink}"
                };

                await _database.Links.AddAsync(newLink);

                if(!(await _database.SaveChangesAsync() >= 0 ))
                {
                    throw new ApplicationException("Nothing was added to the database");
                }

                _response.Success = true;
                _response.Data = new LinkDTO() {  ShortLink = newLink.ShortLink };
                _response.Message = "The link was created successfully";

            }
            catch (Exception)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "There was an internal server error, try again, if the error persist please do comunicate with our it staff";
            }

            return _response;

        }

        public async Task<ServiceResponse<LinkDTO>> GetLinkByShortUrlAsync(string shortUrl)
        {
            ServiceResponse<LinkDTO> _response = new ServiceResponse<LinkDTO>();

            try
            {
                var linkToReturn = await _database.Links.FirstOrDefaultAsync(shortLink => shortLink.ShortLink == shortUrl);

                if(linkToReturn == null)
                {
                    _response.Success = false;
                    _response.Data = null;
                    _response.Message = "The requested link does not exists";
                    return _response;
                }

                _response.Success = true;
                _response.Data = new LinkDTO() { ShortLink = linkToReturn.ShortLink, FullLink = linkToReturn.FullLink };
                _response.Message = "The link was fetch successfully";

            }
            catch (Exception)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "There was an internal server error, try again, if the error persist please do comunicate with our it staff";
            }

            return _response;
        }

        public Task<ServiceResponse<LinkDTO>> DeleteLinkAsync(LinkDTO linkDTO)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerateShortLink(string Url)
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

        private async Task<string> GetLinkTitle(string Url)
        {
            WebClient webClient = new WebClient();
            string pageString = webClient.DownloadString(Url);

            string Title = Regex.Match(pageString, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

            return String.IsNullOrWhiteSpace(Title) ? Title : "";
        }

    }
}
