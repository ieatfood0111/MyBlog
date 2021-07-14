using DEMOMVCC.DAO;
using DEMOMVCC.Models;
using DEMOMVCC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace DEMOMVCC.Controllers
{
    [HandleError(ExceptionType = typeof(NullReferenceException), View = "/Home/Index")]
    public class SearchController : Controller
    {
        ArticleDAO ad = new ArticleDAO();
        UserDAO ud = new UserDAO();
        CommentDAO cd = new CommentDAO();
        // GET: Search

        [HttpGet]
        public ActionResult Index(int? id,string keyword)
        {
            if(id == null || keyword.Length == 0)
            {
                ViewBag.number = 0;
                return PartialView();
            }
            //string keyword = Request.Form["keyword"];
            List<Article> la = ad.searchArticleFull(keyword, id.Value);
            ViewBag.value = id.Value;
            double number = ad.getNumberOfArticleSearch(keyword);
            ViewBag.found = number;
            ViewBag.number = Math.Ceiling(number / 4);
            ViewBag.listArticleSearch = la;
            ViewBag.keyword = keyword;
            return PartialView();
        }

        [HttpGet]
        public ActionResult Key(string keyword)
        {
            List<Article> list =  ad.searchArticleSuggestion(keyword);
            ViewBag.listArticleSearch = list;         
            return PartialView();
        }
    }
}