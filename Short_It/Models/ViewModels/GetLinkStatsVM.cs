using Short_It.DTOs.Link;

namespace Short_It.Models.ViewModels
{
    public class GetLinkStatsVM
    {
        public LinkStatsDTO LinkStats { get; set; }
        public bool IsLinkEmpty { get; set; }
        public bool IsLinkFound { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        
    }
}
