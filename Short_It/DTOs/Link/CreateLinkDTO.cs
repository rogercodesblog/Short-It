using System.ComponentModel.DataAnnotations;

namespace Short_It.DTOs.Link
{
    public class CreateLinkDTO
    {
        [Required]
        public string FullLink { get; set; }
    }
}
