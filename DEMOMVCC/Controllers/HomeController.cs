
using DEMOMVCC.DAO;
using DEMOMVCC.Models;
using DEMOMVCC.Security;
using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DEMOMVCC.Controllers
{
    [HandleError(ExceptionType = typeof(NullReferenceException), View = "/Home/Index")]
    public class HomeController : Controller
    {
        UserDAO ud = new UserDAO();
        ArticleDAO ad = new ArticleDAO();
        public ActionResult Index()
        {

            List<Article> la = ad.getListArticler();
            ViewBag.listArticle = la;
            ViewBag.session = Session["USER_LOGIN_NAME"];
            //if (Session["USER_LOGIN"] == null)
            //{
            //    return View("Login");
            //}
            return View();
        }
        public void Verify()
        {
            String username = Request.Form["username"];
            String password = Request.Form["password"];
            User user = ud.Login(username, password);
            if (user == null)
            {
                Response.Redirect("/Home/Login");
            }
            else
            {
                Session.Add("USER_LOGIN", user.Role);
                Session.Add("USER_LOGIN_NAME", user.Username);
                ViewBag.session = Session["USER_LOGIN"];
                Response.Redirect("/Home/Index");
            }
        }
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public JsonResult AccessRegister(string username, string password, string email, string link, string name, string code)
        {
            if (username == null || password == null || email == null || link == null || name == null || code == null)
            {
                return Json(new
                {
                    check = false,
                    
                });
            }
            code = code.Substring(code.IndexOf(',') + 1);
            byte[] data = Convert.FromBase64String(code);
            ViewBag.username = username;
            ViewBag.password = password;
            ViewBag.linkFacebook = link;
            ViewBag.email = email;
            Boolean check = ud.Register(username, password, email, link, name, data);
            return Json(new
            {
                check = check,
            });
        }           

        public ActionResult DemoUp()
        {

            return PartialView();
        }


        public ActionResult Logout()
        {
            Session.RemoveAll();
            return View("Login");
        }

        public ActionResult Error()
        {

            
            return PartialView();
        }
    }
}