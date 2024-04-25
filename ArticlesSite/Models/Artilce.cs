using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArticlesSite.Models
{
    public class Artilce
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("عنوان المقالة")]
        public string Name { get; set; }

        [Required]
        [DisplayName("محتوى المقالة")]
        public string Content { get; set; }

        [Required]
        [DisplayName("الصورة")]
        public string ImageUrl { get; set; }

        [DisplayName("تاريخ الانشاء")]
        public DateTime Created { get; set; }

        [Required]
        [DisplayName("الصنف")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}