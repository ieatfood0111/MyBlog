using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace DEMOMVCC.Models
{
    public class Article
    {
        int articleID;
        string username;
        string articleName;
        string articleContent;
        string articleAuthor;
        string categorie;
        DateTime createdTime;
        DateTime publishTime;
        int likeNumber;


        public Article()
        {
        }

        public Article(int articleID, string username, string articleName, string articleContent, string articleAuthor, string categorie, DateTime createdTime, DateTime publishTime, int likeNumber)
        {
            this.articleID = articleID;
            this.username = username;
            this.articleName = articleName;
            this.articleContent = articleContent;
            this.articleAuthor = articleAuthor;
            this.categorie = categorie;
            this.createdTime = createdTime;
            this.publishTime = publishTime;
            this.likeNumber = likeNumber;
        }

        public string Username { get => username; set => username = value; }
        public string ArticleName { get => articleName; set => articleName = value; }
        public string ArticleContent { get => articleContent; set => articleContent = value; }
        public string ArticleAuthor { get => articleAuthor; set => articleAuthor = value; }
        public DateTime CreatedTime { get => createdTime; set => createdTime = value; }
        public string Categorie { get => categorie; set => categorie = value; }
        public int LikeNumber { get => likeNumber; set => likeNumber = value; }
        public int ArticleID { get => articleID; set => articleID = value; }
        public DateTime PublishTime { get => publishTime; set => publishTime = value; }
    }
}