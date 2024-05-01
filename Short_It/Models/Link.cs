using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Short_It.Models
{
    [Table("Links")]
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LinkTitle { get; set; }
        [Required(ErrorMessage = "The provided url can't be empty")]
        public string FullLink { get; set; }
        [Required]
        public string ShortLink { get; set; }
        public int TimesVisited { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
