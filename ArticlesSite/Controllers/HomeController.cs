using ArticlesSite.Models;
using ArticlesSite.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArticlesSite.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private List<Artilce> newArticle()
        {
            List<Artilce> newArt = new List<Artilce>();
            var article = db.Artilces.ToList();

            if (article != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    DateTime time = article.Max(x => x.Created);

                    newArt.Add(article.Where(s => s.Created == time).First());

                    article.Remove(article.Where(s => s.Created == time).First());

                }
            }

            return newArt;
        }


        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                Categories = db.Categories.ToList(),
                NewArticles = newArticle(),
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Articles(int id)
        {
            var c = db.Categories.Where(x => x.Id == id).First();
            var articles = db.Artilces.Where(x=>x.Category.Name == c.Name).ToList();


            return View(articles);
        }

        public ActionResult Publishers()
        {
            var user = UserManager.Users.Where(x=> x.Type == "ناشر").ToList();

            var model = new HomeViewModel
            {
                Publishers = user,
            };
            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Panel()
        {
            var model = new HomeViewModel
            {
                Month = db.Artilces.Count(x => x.Created.Month == DateTime.Now.Month),
                Year = db.Artilces.Count(x => x.Created.Year == DateTime.Now.Year),
                Total = db.Artilces.Count()
            };

            return View(model);
        }

        public ActionResult ReadArticles(int id) 
        {
            var art = db.Artilces.Find(id);
        
            return View(art);
        }

        public ActionResult AboutUs()
        {

            return View();
        }
    }
}