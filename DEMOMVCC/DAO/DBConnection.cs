using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DEMOMVCC.fonts
{
    public class DBConnection
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"IEATFOOD0111\SQLEXPRESS";

            string database = "MyBlog";
            string username = "sa";
            string password = "123456";

            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID="
                        + username + ";Password=" + password;
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}