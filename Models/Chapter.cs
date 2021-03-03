using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGAB.Models
{
    public class Chapter
    {
        [Key]
        public int ChapterId { get; set; }

        [Required(ErrorMessage = "You must have a title!")]
        public string ChapterName { get; set; }

        [Required(ErrorMessage = "You must have a content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public DateTime Data { get; set; }
        
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}