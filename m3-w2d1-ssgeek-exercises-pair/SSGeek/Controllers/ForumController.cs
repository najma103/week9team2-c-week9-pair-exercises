using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSGeek.DAL;
using SSGeek.Models;

namespace SSGeek.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult ForumPost()
        {
            ForumPost newPost = new ForumPost();
            newPost.Username = Request.Params["Username"];
            newPost.Subject = Request.Params["Subject"];
            newPost.Message = Request.Params["Message"];
            if (newPost.Username != null && newPost.Subject != null && newPost.Message != null )
            {
                IForumPostDAL DAL = new ForumPostSqlDAL();

                bool success = DAL.SaveNewPost(newPost);
                if (success)
                {
                    ViewBag.message = "Post succssful";
                }
                else
                {
                    ViewBag.message = "No post made ";
                }
            }
            return View("ForumPost");
        }

        public ActionResult ForumView()
        {
            IForumPostDAL DAL = new ForumPostSqlDAL();
            List<ForumPost> postList = DAL.GetAllPosts();
            return View("ForumView", postList);
        }

    }
}