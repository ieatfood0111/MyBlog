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
    public class CommentDAO
    {

        public void Comment(string username, int articleId, string commentContent
            , SqlDateTime createdDate)
        {
            string SQLSelect = "INSERT into [comment] values" +
                " ( @articleId , @username , @commentContent , @createdDate )";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@articleId", SqlDbType.Int).Value = articleId;
                cmd.Parameters.Add("@commentContent", SqlDbType.NVarChar).Value = commentContent;
                cmd.Parameters.Add("@createdDate", SqlDbType.DateTime).Value = createdDate;
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
        public List<Comment> LoadComment(int id)
        {
            string SQLSelect = "Select * from comment where articleId = @articleId";
            SqlConnection conn = DBConnection.GetDBConnection();
            List<Comment> la = null;
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@articleId", SqlDbType.Int).Value = id;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        la = new List<Comment>();
                        while (reader.Read())
                        {
                            la.Add(new Comment
                            {
                               ArticleId = reader.GetInt32(0),
                               Username =reader.GetString(1),
                               CommentContent = reader.GetString(2),
                               CreatedDate = reader.GetDateTime(3),
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
    }
}