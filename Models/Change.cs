using AGAB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGAB.Models
{
    public class Change
    {
        [Key]
        public int ChangeId { get; set; }

        [Required(ErrorMessage = "Invalid content: Cannot be empty!")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int ArticleId { get; set; }

        public string UserId { get; set; }  // Adaug

        public virtual ApplicationUser User { get; set; }   // Adaug

        public virtual Article Article { get; set; }

    }
}