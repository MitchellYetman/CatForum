﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatForum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageFilename {  get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [NotMapped]
        [Display(Name = "Photo")]
        public IFormFile? ImageFile { get; set; }

        // Navigation property
        public List<Comment>? Comments { get; set; }
        
    }
}
