using ArticlesSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticlesSite.ViewModel
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }

        public List<Artilce> NewArticles { get; set; }

        public List<ApplicationUser> Publishers { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int Total { get; set; }

    }
}