using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArticlesSite.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("اسم الصنف")]
        public string Name { get; set; }

        [Required]
        [DisplayName("وصف الصنف")]
        public string Descreption { get; set; }

        public ICollection<Artilce> Articles { get; set; }
    }
}