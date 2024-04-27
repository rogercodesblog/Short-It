using System.ComponentModel.DataAnnotations;

namespace Short_It.DTOs.Link
{
    public class CreateLinkDTO
    {
        [Required(ErrorMessage = "The provided url can't be empty")]
        public string FullLink { get; set; }
    }
}
