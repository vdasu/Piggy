using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Piggy
{
    public partial class Details : System.Web.UI.Page
    {
        private User user;
        private static readonly string connectionString;
        private string restaurantNameParam;
        private string restaurantIdParam;

        static Details()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["piggyDB"].ConnectionString;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["User"];
            if (user == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if(user.isAdmin == true)
            {
                makeReviewPanel.Visible = false;
            }

            restaurantNameParam = Request.QueryString["restaurantName"];
            restaurantIdParam = Request.QueryString["restaurantId"];

            restaurantRatingLabel.Style["display"] = "block";
            restaurantRatingLabel.Style["width"] = "50px";

            if (!IsPostBack)
            {
                // fetch and update views object

                string userViewCookie = Request.Cookies[this.user.userName].Value;
                Views userViews = new JavaScriptSerializer().Deserialize<Views>(userViewCookie);

                string visitedRestaurant = restaurantIdParam + "_" + restaurantNameParam;
                userViews.UpdateMostViewed(visitedRestaurant);

                // delete the existing cookie and add the updated one as new cookie

                Request.Cookies[user.userName].Expires = DateTime.Now.AddDays(-1);

                HttpCookie cookie = new HttpCookie(user.userName);
                cookie.Expires = DateTime.Now.AddMinutes(20);
                string userViewsJson = new JavaScriptSerializer().Serialize(userViews);
                cookie.Value = userViewsJson;
                Response.Cookies.Add(cookie);

                // restaurant descriptions
                getComments(user.userId.ToString(), restaurantIdParam);
                getDescriptionAndRating(restaurantIdParam);
                Tuple<string, string> prevReviewTuple = getPrevReviewIfAny(restaurantIdParam, user.userId.ToString());
                bool newReview;
                if (prevReviewTuple != null)
                {
                    newReview = false;
                    submitComment.Text = "Update";
                    if (prevReviewTuple.Item1 == null)
                    { 
                        commentEntry.Text = "";
                    }
                    else
                    {
                        commentEntry.Text = prevReviewTuple.Item1;
                    }
                    ratingDDL.SelectedIndex = int.Parse(prevReviewTuple.Item2);
                }
                else
                {
                    newReview = true;
                    submitComment.Text = "Submit";
                }
                ViewState["newReview"] = newReview;
            }

            restaurantNameLabel.Text = restaurantNameParam;
        }

        private void getComments(string userId, string restaurantId)
        {
            String sqlQuery = "SELECT UserName, Comment, Rating from Reviews, Users WHERE Users.Id=Reviews.UserID AND RestaurantId=@restaurantId AND Users.Id != @userId AND isApproved=1";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.AddWithValue("@restaurantId", restaurantId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Connection = conn;
                    DataSet ds = new DataSet();
                    using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        adapter.Fill(ds, "comments");
                    }
                    commentsGrid.DataSource = ds.Tables["comments"];
                    commentsGrid.DataBind();
                }
            }
        }

        private void getDescriptionAndRating(string restaurantId)
        {
            String sqlQuery = "SELECT Description, AvgRating FROM Restaurants WHERE Id = @restaurantId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.AddWithValue("@restaurantId", restaurantId);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if(sdr.Read())
                        {
                            restaurantDescription.Text = sdr["Description"].ToString();
                            restaurantRating.Text = sdr["AvgRating"].ToString();
                        }
                    }
                    cmd.CommandText = "UPDATE Restaurants SET [Views] = [Views] + 1 WHERE ID = @restaurantId";;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Tuple<string, string> getPrevReviewIfAny(string restaurantId, string userId)
        {
            string sqlQuery = "SELECT Comment, Rating FROM Reviews WHERE UserId=@userId AND RestaurantId=@restaurantId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@restaurantId", restaurantId);
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            string comment = sdr["Comment"].ToString();
                            string rating = sdr["Rating"].ToString();
                            return new Tuple<string, string>(comment, rating);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        private void updateReview(string restaurantId, string userId, string newComment, string newRating)
        {
            string sqlQuery = "UPDATE Reviews SET Rating=@newRating, Comment=@newComment WHERE RestaurantId=@restaurantId AND UserId=@userId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.AddWithValue("@restaurantId", restaurantId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@newComment", newComment);
                    cmd.Parameters.AddWithValue("@newRating", newRating);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT Description, AvgRating FROM Restaurants WHERE Id = @restaurantId";
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            restaurantDescription.Text = sdr["Description"].ToString();
                            restaurantRating.Text = sdr["AvgRating"].ToString();
                        }
                    }
                }
            }
        }

        private void createReview(string restaurantId, string userId, string comment, string rating)
        {
            string sqlQuery = "INSERT INTO Reviews(UserId, RestaurantId, Comment, Rating) VALUES (@userId, @restaurantId, @comment, @rating)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@restaurantId", restaurantId);
                    cmd.Parameters.AddWithValue("@comment", comment);
                    cmd.Parameters.AddWithValue("@rating", rating);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = "SELECT Description, AvgRating FROM Restaurants WHERE Id = @restaurantId";
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            restaurantDescription.Text = sdr["Description"].ToString();
                            restaurantRating.Text = sdr["AvgRating"].ToString();
                        }
                    }
                }
            }

            ViewState["newReview"] = false;
            submitComment.Text = "Update";
        }

        protected void submitComment_Click(object sender, EventArgs e)
        {
            bool newReview = (bool)ViewState["newReview"];
            if (newReview)
            {
                createReview(restaurantIdParam, user.userId.ToString(), commentEntry.Text, ratingDDL.SelectedValue.ToString());
            }
            else
            {
                updateReview(restaurantIdParam, user.userId.ToString(), commentEntry.Text, ratingDDL.SelectedValue.ToString());
            }
        }
    }
}