using Short_It.DTOs.Link;

namespace Short_It.Models.ViewModels
{
    public class GetLinkStatsVM
    {
        public LinkStatsDTO LinkStats { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsLinkEmpty { get; set; }
        public bool IsLinkNotFound { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        
    }
}
