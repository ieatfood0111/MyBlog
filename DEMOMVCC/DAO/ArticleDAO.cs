using DEMOMVCC.fonts;
using DEMOMVCC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace DEMOMVCC.DAO
{
    public class ArticleDAO
    {

        public Boolean uploadArticle(string username, string articleName, string articleContent
            , string articleAuthor, SqlDateTime createdDate, string catergorie)
        {
            string SQLSelect = "INSERT into [articleWaiting] values" +
                " ( @username , @articleName , @articleContent , @articleAuthor " +
                ", @createdDate , @catergorie , 0) ";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@articleName", SqlDbType.NVarChar, 50).Value = articleName;
                cmd.Parameters.Add("@articleContent", SqlDbType.NVarChar).Value = articleContent;
                cmd.Parameters.Add("@articleAuthor", SqlDbType.NVarChar, 50).Value = articleAuthor;
                cmd.Parameters.Add("@createdDate", SqlDbType.DateTime).Value = createdDate;
                cmd.Parameters.Add("@catergorie", SqlDbType.NVarChar, 50).Value = catergorie;
                cmd.ExecuteNonQuery();
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
            return true;
        }
        public List<Article> getListArticleByUser(string username)
        {
            string SQLSelect = "Select * from article where username = @username";
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
        public List<Article> getListArticler()
        {
            string SQLSelect = "Select top(5)* from article order by createdDate desc";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<Article> la = null;
            conn.Open();
            DataSet dt = new DataSet();
            SqlDataAdapter dAdapt = new SqlDataAdapter("Select * From article", conn);
            dAdapt.Fill(dt, "ơ địt");
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
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
        public Article getArticleById(int id)
        {
            string SQLSelect = "select * from article where articleID = @articleID";
            SqlConnection conn = DBConnection.GetDBConnection();
            Article article = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleID", SqlDbType.Int).Value = id;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            article = new Article
                            {
                                ArticleID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                ArticleName = reader.GetString(2),
                                ArticleAuthor = reader.GetString(4),
                                ArticleContent = reader.GetString(3),
                                CreatedTime = reader.GetDateTime(5),
                                Categorie = reader.GetString(6),
                                LikeNumber = reader.GetInt32(7),
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
            return article;
        }
        public Article getVerifyArticleById(int id)
        {
            string SQLSelect = "select * from articleWaiting where articleID = @articleID";
            SqlConnection conn = DBConnection.GetDBConnection();
            Article article = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleID", SqlDbType.Int).Value = id;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            article = new Article
                            {
                                ArticleID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                ArticleName = reader.GetString(2),
                                ArticleAuthor = reader.GetString(4),
                                ArticleContent = reader.GetString(3),
                                CreatedTime = reader.GetDateTime(5),
                                Categorie = reader.GetString(6),
                                LikeNumber = reader.GetInt32(7),
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
            return article;
        }
        public void updateArticleByID(int id, string articleName, string articleContent
            , string articleAuthor, string catergorie)
        {
            string sql = "Update article set articleName = @articleName , " +
                " articleContent = @articleContent ," +
                " articleAuthor = @articleAuthor ," +
                " catergorie = @catergorie " +
                " where articleId = @articleId ";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.Add("@articleId", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@articleName", SqlDbType.NVarChar).Value = articleName;
                cmd.Parameters.Add("@articleAuthor", SqlDbType.NVarChar).Value = articleAuthor;
                cmd.Parameters.Add("@articleContent", SqlDbType.NVarChar).Value = articleContent;
                cmd.Parameters.Add("@catergorie", SqlDbType.NVarChar).Value = catergorie;
                cmd.ExecuteNonQuery();
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
        }
        public void deleteMember(int articleId)
        {
            string SQLSelect = "delete from article where articleId = @articleId";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleId", SqlDbType.Int).Value = articleId;
                cmd.ExecuteNonQuery();
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
        }
        public void publishArticle(string username, string articleName, string articleContent
            , string articleAuthor, SqlDateTime createdDate, string catergorie, SqlDateTime uploadDate)
        {
            string SQLSelect = "INSERT into [article] values" +
                    " ( @username , @articleName , @articleContent , @articleAuthor " +
                    ", @createdDate , @catergorie , 0 , @uploadDate) ";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@articleName", SqlDbType.NVarChar, 50).Value = articleName;
                cmd.Parameters.Add("@articleContent", SqlDbType.NVarChar).Value = articleContent;
                cmd.Parameters.Add("@articleAuthor", SqlDbType.NVarChar, 50).Value = articleAuthor;
                cmd.Parameters.Add("@createdDate", SqlDbType.DateTime).Value = createdDate;
                cmd.Parameters.Add("@catergorie", SqlDbType.NVarChar, 50).Value = catergorie;
                cmd.Parameters.Add("@uploadDate", SqlDbType.DateTime).Value = uploadDate;
                cmd.ExecuteNonQuery();
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
        }
       

        public List<Article> getListArticlerNewestByPage(int page)
        {
            page = page - 1;
            string SQLSelect = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY createdDate DESC) " +
                "as Number , * FROM Article) as DataSearch where Number between @start and @end ";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<Article> la = null;
            int start = page * 4  + 1;
            int end = start + 4 - 1 ;      
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
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

        public List<Article> getListArticlerMostLikeByPage(int page)
        {
            page = page - 1;
            string SQLSelect = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY likeNumber DESC) " +
                "as Number , * FROM Article) as DataSearch where Number between @start and @end ";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<Article> la = null;
            int start = page * 4 + 1;
            int end = start + 4 - 1;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
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

        public double getNumberOfArticle()
        {
            string SQLSelect = "SELECT COUNT(*) FROM article";
            SqlConnection conn = DBConnection.GetDBConnection();
            int size = 0;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
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
            return (double) size ;
        }

        public Article getArticleFromWaiting(int id)
        {
            string SQLSelect = "select * from articleWaiting where articleID = @articleID";
            SqlConnection conn = DBConnection.GetDBConnection();
            Article article = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleID", SqlDbType.Int).Value = id;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            article = new Article
                            {
                                ArticleID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                ArticleName = reader.GetString(2),
                                ArticleAuthor = reader.GetString(4),
                                ArticleContent = reader.GetString(3),
                                CreatedTime = reader.GetDateTime(5),
                                Categorie = reader.GetString(6),
                                LikeNumber = reader.GetInt32(7),
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
            return article;
        }
        public List<Article> getListArticlerWaiting()
        {
            string SQLSelect = "Select * from articleWaiting ";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<Article> la = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
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

        public void deleteArticleWaiting(int articleId)
        {
            string SQLSelect = "delete from articleWaiting where articleId = @articleId";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleId", SqlDbType.Int).Value = articleId;
                cmd.ExecuteNonQuery();
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
        }
        public int getLike(int? id)
        {

            string SQLSelect = "select * from article where articleID = @articleID";
            SqlConnection conn = DBConnection.GetDBConnection();
            int article = 0;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleID", SqlDbType.Int).Value = id;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            article = reader.GetInt32(7);
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
            return article;
        }
        public void like(int? id,int like)
        {

            string SQLSelect = "UPDATE article SET likeNumber = @like WHERE articleId = @articleId";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleId", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@like", SqlDbType.Int).Value = like;
                cmd.ExecuteNonQuery();
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
        }

        public void likeArticle(int id , string username,Boolean liked)
        {
            string SQLSelect = "INSERT into [liked] values" +
                    " ( @id , @username , @liked) ";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@liked", SqlDbType.Bit).Value = liked;
                cmd.ExecuteNonQuery();
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
        }



        public Boolean checkLike(int articleId, string username)
        {

            string SQLSelect = "select * from liked where articleID = @articleID and username = @username";
            SqlConnection conn = DBConnection.GetDBConnection();
            Boolean liked = false;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleID", SqlDbType.Int).Value = articleId;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {                           
                            liked = reader.GetBoolean(2);
                        }
                    }
                }
                return liked;
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
            return liked;
        }
        public Boolean checkExistedLiked(int articleId, string username)
        {

            string SQLSelect = "select * from liked where articleID = @articleID and username = @username";
            SqlConnection conn = DBConnection.GetDBConnection();
            Boolean liked = false;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleID", SqlDbType.Int).Value = articleId;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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
            return liked;
        }
        public void setLike(int articleId, string username,Boolean liked)
        {
           
            string SQLSelect = "UPDATE liked SET liked = @liked where articleID = @articleID and username = @username";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@liked", SqlDbType.Bit).Value = liked;
                cmd.Parameters.Add("@articleID", SqlDbType.Int).Value = articleId;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                cmd.ExecuteNonQuery();
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
        }
        public List<Article> searchArticleSuggestion(string keyword)
        {
            
            string SQLSelect = "    SELECT Top(5)* FROM  article where articleName like '%"+ keyword + "%' or articleContent like '%" + keyword + "%'     ";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<Article> la = null;    
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
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
                                PublishTime = reader.GetDateTime(8),
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
        public List<Article> searchArticleFull(string keyword, int page)
        {
            page = page - 1;
            string SQLSelect = "SELECT * FROM (SELECT ROW_NUMBER() " +
                " OVER(ORDER BY likeNumber DESC) as Number , " +
                " * FROM (SELECT * FROM  article where articleName like '%" + keyword + "%' or articleContent like '%" + keyword + "%') " +
                " as data) as DataSearch where Number between @start and @end";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<Article> la = null;
            int start = page * 4 + 1;
            int end = start + 4 - 1;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
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
        public double getNumberOfArticleSearch(string keyword)
        {
            string SQLSelect = "SELECT COUNT(*) FROM article where articleName like '%" + keyword + "%' or articleContent like '%" + keyword + "%'";
            SqlConnection conn = DBConnection.GetDBConnection();
            int size = 0;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
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

    }
}