using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGAB.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "You must have a name for your new category!")]
        public string CategoryName { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}