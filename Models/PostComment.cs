using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Models
{
    public class PostComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Post Id")]
        public int PostId { get; set; }

        [Required]
        [DisplayName("Original Poster")]
        public string OriginalPoster { get; set; }

        [Required]
        [DisplayName("Day published")]
        public DateTime Comment_Day_published { get; set; }

        [DisplayName("Text")]
        public string CommentText { get; set; }
    }
}
