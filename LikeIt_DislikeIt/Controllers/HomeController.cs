using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LikeIt_DislikeIt.Models;
using System.Data.SqlClient;

namespace LikeIt_DislikeIt.Controllers
{
    public class HomeController : Controller
    {
        // Replaces the Index of the ASP.NET with an image that randomised everytime the user refreshes 
        // the page. 
        public ActionResult Index()
        {
            Random rnd = new Random();
            Image img = new Image();
            using (DbModels db = new DbModels())
            {
                img = db.Images.Find(rnd.Next(1,11));
            }
            return View(img);
        }

        // Made a ProcessForm which will test whether the user click on like or dislike button.
        public ActionResult ProcessForm(FormCollection collection)
        {
            string connectionString = "data source=.; database = Sample; integrated security=SSPI";
            string title = collection.Get("title");
            string like = collection.Get("like");
            string dislike = collection.Get("dislike");
            if (!string.IsNullOrEmpty(like))
            {
                Like likes = new Like();
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    SqlDataAdapter sqlCmd = new SqlDataAdapter();
                    sqlCmd.InsertCommand = new SqlCommand("INSERT INTO Likes (Title, [Like], Dislike) VALUES (@title, @like, @dislike)");
                    sqlCmd.InsertCommand.Connection = sqlCon;
                    sqlCmd.InsertCommand.Parameters.AddWithValue("@title", title);
                    sqlCmd.InsertCommand.Parameters.AddWithValue("@like", 1);
                    sqlCmd.InsertCommand.Parameters.AddWithValue("@dislike", 0);
                    sqlCon.Open();
                    sqlCmd.InsertCommand.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            if (!string.IsNullOrEmpty(dislike))
            {
                Like likes = new Like();
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    SqlDataAdapter sqlCmd = new SqlDataAdapter();
                    sqlCmd.InsertCommand = new SqlCommand("INSERT INTO Likes (Title, [Like], Dislike) VALUES (@title, @like, @dislike)");
                    sqlCmd.InsertCommand.Connection = sqlCon;
                    sqlCmd.InsertCommand.Parameters.AddWithValue("@title", title);
                    sqlCmd.InsertCommand.Parameters.AddWithValue("@like", 0);
                    sqlCmd.InsertCommand.Parameters.AddWithValue("@dislike", 1);
                    sqlCon.Open();
                    sqlCmd.InsertCommand.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult History(LikeModel like)
        {
            string connectionString = "data source=.; database = Sample; integrated security=SSPI";
            ViewBag.Message = "Your like history.";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                SqlDataAdapter sqlCmd = new SqlDataAdapter();
                sqlCmd.SelectCommand = new SqlCommand("SELECT Title FROM Likes WHERE [Like] = 1");
                sqlCmd.SelectCommand.Connection = sqlCon;
                sqlCon.Open();
                SqlDataReader reader = sqlCmd.SelectCommand.ExecuteReader();
                List<LikeModel> model = new List<LikeModel>();
                while (reader.Read())
                {
                    var details = new LikeModel();
                    details.Title = reader["Title"].ToString();
                    model.Add(details);
                }
                return View("History", model);
            }
        }
    }
}