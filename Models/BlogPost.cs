using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayName("Day published")]
        public DateTime Day_published { get; set; }

        [DisplayName("Image url")]
        public string ImageUrl { get; set; }

        public string Text { get; set; }
    }
}
