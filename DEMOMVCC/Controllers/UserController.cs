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
    public class UserController : Controller
    {
        ArticleDAO ad = new ArticleDAO();
        UserDAO ud = new UserDAO();
        CommentDAO cd = new CommentDAO();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [LoginFilter(Roles = "1,2,3")]
        public ActionResult LikedArticles(int? id)
        {
            List<Article> la = ud.ArticlesLiked((string)Session["USER_LOGIN_NAME"], id.Value);
            double number = ud.getNumberOfArticle((string)Session["USER_LOGIN_NAME"]);
            ViewBag.value1 = (string)Session["USER_LOGIN_NAME"];
            ViewBag.number = Math.Ceiling(number / 4);
            ViewBag.value = id.Value;
            ViewBag.listArticle = la;
            return View();
        }
        [LoginFilter(Roles = "1,2,3")]
        public void Comment(int? id)
        {
            string comment = Request.Form["comment"];
            string username = (string)Session["USER_LOGIN_NAME"];
            cd.Comment(username, id.Value, comment, DateTime.Now);
            Response.Redirect("/Article/Detail/" + id.Value);
        }

        [HttpGet]
        public ActionResult Detail()
        {
            string uri = Request.Url.AbsoluteUri;
            Uri myUri = new Uri(uri);
            string username = HttpUtility.ParseQueryString(myUri.Query).Get("username");
            string follow = (string)Session["USER_LOGIN_NAME"];
            User user = ud.getUser(username);
            ViewBag.followed = new FollowerDAO().checkFollow(follow, username);
            ViewBag.listArticle = ud.getListArticleByUser(username);
            if (follow != null)
            {
                if (follow.Equals(username))
                {
                
                    return View("Me", user);
                }
                else
                {
                 
                    return View(user);
                }
            }
            else
            {
        
                return View(user);
            }
        }
        [HttpGet]
        [LoginFilter(Roles = "1,2,3")]
        public void Follow()
        {
            string uri = Request.Url.AbsoluteUri;
            Uri myUri = new Uri(uri);
            string follow = (string)Session["USER_LOGIN_NAME"];
            
            string follower = HttpUtility.ParseQueryString(myUri.Query).Get("follower");
          
            
                new FollowerDAO().Follow(follow, follower);
                Response.Redirect("/User/Detail?username=" + follower);
            
        }
        [HttpGet]
        [LoginFilter(Roles = "1,2,3")]
        public void UnFollow()
        {
            string uri = Request.Url.AbsoluteUri;
            Uri myUri = new Uri(uri);
            string follow = (string)Session["USER_LOGIN_NAME"];
            string follower = HttpUtility.ParseQueryString(myUri.Query).Get("follower");
            new FollowerDAO().unFollow(follow, follower);
            Response.Redirect("/User/Detail?username=" + follower);
        }
        [HttpGet]
  
        
        [LoginFilter(Roles = "1,2,3")]
        public ActionResult Following()
        {
            ViewBag.Following = ud.getListFollowing((string)Session["USER_LOGIN_NAME"]);
            return View();
        }
        public ActionResult uploadImage()
        {
            return View();
        }
        [HttpPost]
        public JsonResult upImage(string data)
        {
            ViewBag.image = data;
            return Json(new
            {
                data = data
            }) ;
        }



        public ActionResult PopularMember()
        {
            ViewBag.Following = ud.getPupularMember();
            return View();
        }

        public JsonResult ChangeAvatar(string code)
        {
            if (code == null)
            {
                return Json(new
                {
                    check = false,
                });
            }
            string username = (string)Session["USER_LOGIN_NAME"];
            string codeaf = code.Substring(code.IndexOf(',') + 1);
            byte[] data = Convert.FromBase64String(codeaf);
            Boolean check= ud.updateAvatar(data, username);
            return Json(new
            {
                check = check,           
                code = code
            }) ;
        }
    }
}