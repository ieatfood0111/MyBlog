using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DEMOMVCC.Models
{
    public class User
    {
        String username;
        String password;
        String email;
        String linkFacebook;
        int role;
        string name;
        int follower;
        int articlesNumber;
        int likeNumber;
        string linkAvatar;
            public User()
        {

        }

        public User(string username, string password, string email, string linkFacebook)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.linkFacebook = linkFacebook;
        }

        public User(string username, string password, string email, string linkFacebook, int role)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.linkFacebook = linkFacebook;
            this.role = role;
        }

        public User(string username, string password, string email, string linkFacebook, int role, int follower)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.linkFacebook = linkFacebook;
            this.role = role;
            this.follower = follower;
        }

        public User(string username, string password, string email, string linkFacebook, int role, string name, int follower)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.linkFacebook = linkFacebook;
            this.role = role;
            this.Name = name;
            this.follower = follower;
        }

        public User(string username, string password, string email, string linkFacebook, int role, string name, int follower, int articlesNumber, int likeNumber)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.linkFacebook = linkFacebook;
            this.role = role;
            this.name = name;
            this.follower = follower;
            this.ArticlesNumber = articlesNumber;
            this.LikeNumber = likeNumber;
        }

        public User(string username, string password, string email, string linkFacebook, int role, string name, int follower, int articlesNumber, int likeNumber, string linkAvatar)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.linkFacebook = linkFacebook;
            this.role = role;
            this.name = name;
            this.follower = follower;
            this.articlesNumber = articlesNumber;
            this.likeNumber = likeNumber;
            this.linkAvatar = linkAvatar;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string LinkFacebook { get => linkFacebook; set => linkFacebook = value; }
        public int Role { get => role; set => role = value; }
        public int Follower { get => follower; set => follower = value; }
        public string Name { get => name; set => name = value; }
        public int ArticlesNumber { get => articlesNumber; set => articlesNumber = value; }
        public int LikeNumber { get => likeNumber; set => likeNumber = value; }
        public string LinkAvatar { get => linkAvatar; set => linkAvatar = value; }
    }
}