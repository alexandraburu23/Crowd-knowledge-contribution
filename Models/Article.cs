using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //adugat
using System.Data.Entity; //adaugat
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGAB.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must have a title!")]
        [StringLength(50, ErrorMessage = "Maximum limit: 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must have content for the article!")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "You must have a category!")]
        public int CategoryId { get; set; }

        public string UserId { get; set; } // Adaug 

        public virtual ApplicationUser User { get; set; }  // Adaug

        public bool Protected { get; set; } // Adaug

        public virtual Category Category { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }

        public virtual ICollection<Change> Changes { get; set; }

        public IEnumerable<SelectListItem> Categ { get; set; }
    }
}