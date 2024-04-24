using AutoMapper;

namespace Short_It.DTOs
{
    public class DTOMapping :Profile
    {
        public DTOMapping()
        {
            CreateMap<Models.Link, DTOs.Link.LinkDTO>().ReverseMap();
            CreateMap<Models.Link, DTOs.Link.CreateLinkDTO>().ReverseMap();
        }
    }
}
