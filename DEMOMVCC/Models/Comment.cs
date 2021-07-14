using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DEMOMVCC.Models
{
    public class Comment
    {
        int articleId;
        string username;
        string commentContent;
        DateTime createdDate;

        public Comment(int articleId, string username, string comment, DateTime createdDate)
        {
            this.articleId = articleId;
            this.username = username;
            this.commentContent = comment;
            this.createdDate = createdDate;
        }

        public Comment()
        {
        }

        public int ArticleId { get => articleId; set => articleId = value; }
        public string Username { get => username; set => username = value; }
        public string CommentContent { get => commentContent; set => commentContent = value; }
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }
    }
}