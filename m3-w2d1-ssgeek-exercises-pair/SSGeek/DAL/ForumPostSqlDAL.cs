using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSGeek.Models;
using System.Data.SqlClient;

namespace SSGeek.DAL
{
    public class ForumPostSqlDAL : IForumPostDAL
    {
        string connectionString = @"Data Source=DESKTOP-BQON135\SQLEXPRESS;Initial Catalog=AlienDB;Integrated Security=True";
        string SQL_SelectForum = @"select id, username, subject, message, post_date FROM forum_post";
        string SQL_Insert_Request = @"insert into forum_post values(@user_name, @subject, @message, getdate())";
        public List<ForumPost> GetAllPosts()
        {
            List<ForumPost> forumPost = new List<ForumPost>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SelectForum, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ForumPost post = new ForumPost();
                        post.Id = Convert.ToInt32(reader["id"]);
                        post.Username = Convert.ToString(reader["username"]);
                        post.Subject = Convert.ToString(reader["subject"]);
                        post.Message = Convert.ToString(reader["message"]);
                        post.PostDate = Convert.ToDateTime(reader["post_date"]);

                        forumPost.Add(post);
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw;
            }

            return forumPost;
        }

        public bool SaveNewPost(ForumPost post)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_Insert_Request, conn);
                cmd.Parameters.AddWithValue("@user_name", post.Username);
                cmd.Parameters.AddWithValue("@subject", post.Subject);
                cmd.Parameters.AddWithValue("@message", post.Message);
                //cmd.Parameters.AddWithValue("@postdate", "getdate()");
                int rowsReturned = cmd.ExecuteNonQuery();

                return rowsReturned > 0;
            }
        }
    }
}