using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CatForum.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ImageFilename { get; set; } = string.Empty;

        [NotMapped]
        [Display(Name = "Photo")]
        public IFormFile? ImageFile;
    }
}
