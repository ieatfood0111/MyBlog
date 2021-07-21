using DEMOMVCC.fonts;
using DEMOMVCC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DEMOMVCC.DAO
{
    public class UserDAO 
    {
        
        public User Login(String username,String password)
        {
            string SQLSelect = "SELECT * FROM [User] where username like @username and password like @password ";
            User user = null;
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new User
                            {
                                Username = reader.GetString(0),
                                Password = reader.GetString(1),
                                Email = reader.GetString(2),
                                LinkFacebook = reader.GetString(3),
                                Role = reader.GetInt32(4),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine(ex.StackTrace);    

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return user;
        }

        public Boolean Register(string username, string password, string email, string linkFacebook ,string name, byte[] data)
        {
            string SQLSelect = "INSERT into [User] values ( @username , @password , @email , @linkFacebook , @role , @name , 0  ,@data) ";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar,50).Value = username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = password;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                cmd.Parameters.Add("@linkFacebook", SqlDbType.VarChar, 50).Value = linkFacebook;
                cmd.Parameters.Add("@role", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = data;              
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public List<Article> ArticlesLiked(string username,int page)
        {
            page = page - 1;
            string SQLSelect = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY likeNumber DESC) as Number " +
                ", * FROM (Select a.articleID,a.username,a.articleName,a.articleContent,a.articleAuthor,a.createdDate,a.catergorie,a.likeNumber,a.uploadDate" +
                " from article a inner join liked b on a.articleID = b.articleID left join [user] c on b.username = c.username where b.liked =1 and c.username = @username) " +
                " as article1) as DataSearch where Number between @start and @end";
            SqlConnection conn = DBConnection.GetDBConnection(); 
            int start = page * 4 + 1;
            int end = start + 4 - 1;
            List<Article> la = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@start", SqlDbType.Int).Value = start;
                cmd.Parameters.Add("@end", SqlDbType.Int).Value = end;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        la = new List<Article>();
                        while (reader.Read())
                        {
                            la.Add(new Article
                            {
                                ArticleID = reader.GetInt32(1),
                                Username = reader.GetString(2),
                                ArticleName = reader.GetString(3),
                                ArticleAuthor = reader.GetString(4),
                                ArticleContent = reader.GetString(5),
                                CreatedTime = reader.GetDateTime(6),
                                Categorie = reader.GetString(7),
                                LikeNumber = reader.GetInt32(8),
                                PublishTime = reader.GetDateTime(9),
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return la;
        }
        public double getNumberOfArticle(string username)
        {
            string SQLSelect = "SELECT COUNT(*) FROM (Select a.* from article a inner join liked b on a.articleID = b.articleID left join [user] c on b.username = c.username where b.liked =1 and c.username = @username) as data";
            SqlConnection conn = DBConnection.GetDBConnection();
            int size = 0;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            size = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return (double)size;
        }

        public User getUser(string username)
        {
            string SQLSelect = "select * from [user] where username = @username";
            SqlConnection conn = DBConnection.GetDBConnection();
            User user = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string link = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])reader.GetValue(7));
                            user = new User
                            {
                                Username = reader.GetString(0),
                                Password = reader.GetString(1),
                                Email = reader.GetString(2),
                                LinkFacebook = reader.GetString(3),
                                Role = reader.GetInt32(4),
                                Name = reader.GetString(5),
                                Follower = reader.GetInt32(6),
                                ArticlesNumber = getArticleNumber(username),
                                LikeNumber = getLikeNumber(username),
                                //
                                LinkAvatar = link,
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return user;
        }
        public int getLikeNumber(string username)
        {
            string SQLSelect = "select SUM(likeNumber) from article where username= @username ";
            SqlConnection conn = DBConnection.GetDBConnection();
            int size = 0;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            size = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return size;
        }
        public int getArticleNumber(string username)
        {
            string SQLSelect = "SELECT COUNT(*) FROM article where username = @username ";
            SqlConnection conn = DBConnection.GetDBConnection();
            int size = 0;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            size = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return size;         
        }
        public List<Article> getListArticleByUser(string username)
        {
            string SQLSelect = "Select * from article where username = @username ";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<Article> la = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        la = new List<Article>();
                        while (reader.Read())
                        {
                            la.Add(new Article
                            {
                                ArticleID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                ArticleName = reader.GetString(2),
                                ArticleAuthor = reader.GetString(3),
                                ArticleContent = reader.GetString(4),
                                CreatedTime = reader.GetDateTime(5),
                                Categorie = reader.GetString(6),
                                LikeNumber = reader.GetInt32(7),
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return la;
        }
        public List<User> getListFollowing(string username)
        {
            string SQLSelect = "Select * from [user] a inner join follow b on a.username = b.username2 where b.username1 = @username ";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<User> la = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        la = new List<User>();
                        while (reader.Read())
                        {
                            string link = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])reader.GetValue(7));
                            la.Add(new User
                            {
                                Username = reader.GetString(0),
                                Password = reader.GetString(1),
                                Email = reader.GetString(2),
                                LinkFacebook = reader.GetString(3),
                                Role = reader.GetInt32(4),
                                Name = reader.GetString(5),
                                Follower = reader.GetInt32(6),
                                ArticlesNumber = getArticleNumber(username),
                                LikeNumber = getLikeNumber(username),
                                //data:image/jpeg;base64,                                
                                LinkAvatar = link,
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return la;
        }

        public List<User> getPupularMember()
        {

            string SQLSelect = "select * from [user] a inner join(select top(5)Sum(likenumber) as 'like',username from article group by username order by 'like' desc) b on a.username = b.username "; 
            SqlConnection conn = DBConnection.GetDBConnection();
            List<User> la = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        la = new List<User>();
                        while (reader.Read())
                        {
                            string link = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])reader.GetValue(7));
                            la.Add(new User
                            {
                                Username = reader.GetString(0),
                                Password = reader.GetString(1),
                                Email = reader.GetString(2),
                                LinkFacebook = reader.GetString(3),
                                Role = reader.GetInt32(4),
                                Name = reader.GetString(5),
                                Follower = reader.GetInt32(6),
                                ArticlesNumber = getArticleNumber(reader.GetString(0)),
                                LikeNumber = getLikeNumber(reader.GetString(0)),
                                //data:image/jpeg;base64,                                
                                LinkAvatar = link,
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return la;
        }
        public Boolean changeAvatar(byte[] data,string username)
        {

            string SQLSelect = "update user set linkAvatar = @data where username = @username";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = data;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public Boolean updateAvatar(byte[] data, string username)
        {
            string sql = "UPDATE [user] SET avatar = @data WHERE username = @username";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = data;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
    }
}