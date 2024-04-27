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
                if (!IsValidUrl(createLinkDTO.FullLink))
                {
                    _response.Success = false;
                    _response.Data = null;
                    _response.Message = "The provided url is not valid, please provide a full url";
                    return _response;
                }

                var _linkTitle = await GetLinkTitle(createLinkDTO.FullLink);
                var _generatedShortLink = await GenerateShortLink(createLinkDTO.FullLink);

                Link newLink = new Link()
                {
                    DateCreated = DateTime.Now,
                    FullLink = createLinkDTO.FullLink,
                    ShortLink = _generatedShortLink,
                    LinkTitle = !string.IsNullOrWhiteSpace(_linkTitle) ? _linkTitle : $"Generated Link {_generatedShortLink}"
                };

                await _database.Links.AddAsync(newLink);

                if (!(await _database.SaveChangesAsync() >= 0))
                {
                    throw new ApplicationException("Nothing was added to the database");
                }

                _response.Success = true;
                _response.Data = new LinkDTO() { ShortLink = newLink.ShortLink };
                _response.Message = "The link was created successfully";

            }
            catch (ApplicationException exception)
            {
                _response.Success = false;
                _response.IsInteralError = true;
                _response.Data = null;
                _response.Message = exception.Message;
            }
            catch (Exception)
            {
                _response.Success = false;
                _response.IsInteralError = true;
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
                var _linkToReturn = await _database.Links.FirstOrDefaultAsync(shortLink => shortLink.ShortLink == shortUrl);

                if (_linkToReturn == null)
                {
                    _response.Success = false;
                    _response.Data = null;
                    _response.Message = "The requested link does not exists";
                    return _response;
                }

                _response.Success = true;
                _response.Data = new LinkDTO() { ShortLink = _linkToReturn.ShortLink, FullLink = _linkToReturn.FullLink };
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

        public async Task<ServiceResponse<string>> DeleteLinkAsync(LinkDTO linkDTO)
        {
            ServiceResponse<string> _response = new ServiceResponse<string>();

            try
            {
                var _linkToDelete = await _database.Links.FirstOrDefaultAsync(link => link.ShortLink == linkDTO.ShortLink);

                if (_linkToDelete == null)
                {
                    _response.Success = false;
                    _response.Message = "The link was not found";
                    return _response;
                }

                _database.Links.Remove(_linkToDelete);

                if (!(await _database.SaveChangesAsync() >= 0))
                {
                    throw new ApplicationException("There was an error deleteing the link");
                }

                _response.Success = true;
                _response.Message = "The link was deleted successfully";

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "There was an error deleteing the link";
            }

            return _response;
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

        private bool IsValidUrl(string Url)
        {
            return Regex.IsMatch(Url, @"((http|https)://)(www.)?" + "[a-zA-Z0-9@:%._\\+~#?&//=]" + "{2,256}\\.[a-z]" + "{2,6}\\b([-a-zA-Z0-9@:%" + "._\\+~#?&//=]*)");
        }

    }
}
