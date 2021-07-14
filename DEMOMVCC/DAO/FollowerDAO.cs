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
    public class FollowerDAO
    {
        public void Follow(string follow, string follower)
        {
            addFollow(follower);
            string SQLSelect = "INSERT into [follow] values( @username1 , @username2 ) ";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username1", SqlDbType.VarChar, 50).Value = follow;
                cmd.Parameters.Add("@username2", SqlDbType.VarChar, 50).Value = follower;
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
        public void addFollow(string username)
        {
            string SQLSelect = "UPDATE [user] SET follower = @follower where username = @username ";
            SqlConnection conn = DBConnection.GetDBConnection();
            int follower = getFollower(username);
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@follower", SqlDbType.Int).Value = follower + 1;
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

        public void subFollow(string username)
        {
            string SQLSelect = "UPDATE [user] SET follower = @follower where username = @username";
            SqlConnection conn = DBConnection.GetDBConnection();
            int follower = getFollower(username);
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@follower", SqlDbType.Int).Value = follower - 1;
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
        public Boolean checkFollow(string follow, string follower)
        {
            string SQLSelect = "Select * from follow where username1 = @username1 and username2 = @username2 ";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username1", SqlDbType.VarChar, 50).Value = follow;
                cmd.Parameters.Add("@username2", SqlDbType.VarChar, 50).Value = follower;
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
          
            return false;
        }
        public void unFollow(string follow, string follower)
        {
            subFollow(follower);
            string SQLSelect = "delete from follow where username1 = @username1 and username2 = @username2";
            SqlConnection conn = DBConnection.GetDBConnection();
            conn.Open();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = SQLSelect;
                cmd.Parameters.Add("@username1", SqlDbType.VarChar, 50).Value = follow;
                cmd.Parameters.Add("@username2", SqlDbType.VarChar, 50).Value = follower;
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
        public int getFollower(string username)
        {
            
            string SQLSelect = "select * from [user] where username = @username";
            SqlConnection conn = DBConnection.GetDBConnection();
            int article = 0;
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
                            article = reader.GetInt32(6);
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
    }
}