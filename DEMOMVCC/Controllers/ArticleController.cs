using DEMOMVCC.Models;
using DEMOMVCC.Security;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;
using Newtonsoft.Json;
using HtmlAgilityPack;
using javax.swing.text.html;
using System.Web.Helpers;

namespace DEMOMVCC.DAO
{
   [CustomException]
    public class ArticleController : Controller
    {

        ArticleDAO ad = new ArticleDAO();

        public ActionResult Index()
        {
            return View();
        }
        [LoginFilter(Roles = "1,2,3")]
        public ActionResult Upload()
        {
            return View();
        }
        [LoginFilter(Roles = "1,2,3")]
        //public void AccessUpload()
        //{
        //    string username = (string)Session["USER_LOGIN_NAME"];
        //    string articleName = Request.Form["articleName"];
        //    string articleContent = Request.Form["articleContent"];
        //    string articleAuthor = Request.Form["articleAuthor"];
        //    SqlDateTime createdTime = DateTime.Now;
        //    string catergorie = Request.Form["catergorie"];

        //    ad.uploadArticle(username, articleName, articleContent, articleAuthor, createdTime, catergorie);


        //    Response.Redirect("/Home/Index");
        //}

        [HttpPost]
        public JsonResult AccessUpload(string ArticleName,string ArticleContent, string ArticleAuthor,string Categorie)
        {

            if(ArticleName.Trim().Length == 0 || ArticleContent.Trim().Length == 0 || ArticleAuthor.Trim().Length == 0 || Categorie.Trim().Length == 0)
            {
                return Json(new
                {
                    messageFail = "Cant let input empty",
                    check = false
                });
            }

            string username = (string)Session["USER_LOGIN_NAME"];
            string articleName = ArticleName;          
            string articleContent =ArticleContent;            
            string articleAuthor = ArticleAuthor;
            SqlDateTime createdTime = DateTime.Now;
            string catergorie = Categorie;
            Boolean check = ad.uploadArticle(username, articleName, articleContent, articleAuthor, createdTime, catergorie);
            return Json(new
            {
                messageSucceed = "Upload Success",
                messageFail = "Upload Fail",
                check = check
            }); 
           
        }
        [LoginFilter(Roles = "1,2,3")]
        public ActionResult ListArticle()
        {
            string username = (string)Session["USER_LOGIN_NAME"];
            List<Article> la = ad.getListArticleByUser(username);
            ViewBag.listArticle = la;
            return View();
        }

        [AllowAnonymous]
       
        public ActionResult Detail(int? id)
        {
            if(id == null)
            {
                return View("/Home/Index");
            }
            Boolean liked = ad.checkLike(id.Value, (string)Session["USER_LOGIN_NAME"]);
            ViewBag.liked = liked;
            Article article = ad.getArticleById(id.Value);
            List<Comment> listComment = new CommentDAO().LoadComment(id.Value);
            ViewBag.comments = listComment;
            ViewBag.content = article.ArticleContent;
            return View(article);
        }
        [LoginFilter(Roles = "1,2,3")]
        public ActionResult Edit(int? id)
        {

            Article article = ad.getArticleById(id.Value);

            return View(article);
        }
        [LoginFilter(Roles = "1,2,3")]
        public void AccessEdit(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Home/Index");
                return;
            }
            string articleName = Request.Form["articleName"];
            string articleContent = Request.Form["articleContent"];
            string articleAuthor = Request.Form["articleAuthor"];
            string catergorie = Request.Form["catergorie"];

            ad.updateArticleByID(id.Value, articleName, articleContent, articleAuthor, catergorie);

            Response.Redirect("/Article/ListArticle");
        }
        [LoginFilter(Roles = "1,2,3")]
        public void Delete(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Home/Index");
                return;
            }
            ad.deleteMember(id.Value);
            Response.Redirect("/Article/ListArticle");
        }

        [AllowAnonymous]
        public ActionResult ListArticleNewest(int? id)
        {
            if (id == null)
            {
                return View("/Home/Index");
            }
            List<Article> la = ad.getListArticlerNewestByPage(id.Value);
            ViewBag.value = id.Value;
            double number = ad.getNumberOfArticle();
            ViewBag.number = Math.Ceiling(number /4);
            ViewBag.listArticle = la;
            return View();
        }
        [AllowAnonymous]
        public ActionResult ListArticleByLike(int? id)
        {
            if (id == null)
            {
                return View("/Home/Index");
            }
            List<Article> la = ad.getListArticlerMostLikeByPage(id.Value);
            ViewBag.value = id.Value;
            double number = ad.getNumberOfArticle();
            ViewBag.number = Math.Ceiling(number / 4);
            ViewBag.listArticle = la;
            return View();
        }

        [LoginFilter(Roles ="1,2")]
        public ActionResult VerifyArticle()
        {

            List<Article> la = ad.getListArticlerWaiting();
            ViewBag.listArticle = la;
            return View();
        }
        [LoginFilter(Roles = "1,2")]
        public ActionResult DetailArticle(int? id)
        {
            if (id == null)
            {
                return View("/Home/Index");
            }
            Article article = ad.getVerifyArticleById(id.Value);
            ViewBag.content = article.ArticleContent;
            return View(article);
        }
        [LoginFilter(Roles = "1,2")]
        public void AccessVerifyArticle(int ?id)
        {
            if (id == null)
            {
                Response.Redirect("/Home/Index");
                return;
            }
            Article wa = ad.getArticleFromWaiting(id.Value);
            ad.publishArticle(wa.Username, wa.ArticleName, wa.ArticleContent,
                wa.ArticleAuthor, wa.CreatedTime, wa.Categorie, DateTime.Now);
           ad.deleteArticleWaiting(wa.ArticleID);
            Response.Redirect("/Article/VerifyArticle");
        }
        [LoginFilter(Roles = "1,2")]
        public void DeleteArticle(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Home/Index");
                return;
            }
            Article wa = ad.getArticleFromWaiting(id.Value);
            ad.deleteArticleWaiting(wa.ArticleID);
            Response.Redirect("/Article/VerifyArticle");
        }
        [LoginFilter(Roles = "1,2,3")]
        public void Like(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Home/Index");
                return;
            }
            string username = (string)Session["USER_LOGIN_NAME"];
            int like = ad.getLike(id.Value);

            Boolean liked = ad.checkLike(id.Value, username);
            if (!ad.checkExistedLiked(id.Value, username))
            {
                ad.likeArticle(id.Value, username, true);
            }
            else
            {
                ad.setLike(id.Value, username, true);
            }         
            ad.like(id.Value, like + 1);
            Response.Redirect("/Article/Detail/"+id);
        }
        [LoginFilter(Roles = "1,2,3")]
        public void DisLike(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Home/Index");
                return;
            }
            string username = (string)Session["USER_LOGIN_NAME"];
            int like = ad.getLike(id.Value);

            ad.setLike(id.Value, username, false);
            ad.like(id.Value, like - 1);

            Response.Redirect("/Article/Detail/" + id);
        }
    }
}