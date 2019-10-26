using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Piggy
{
    public partial class AdminHomePage : Page
    {
        private static readonly string connectionString;
        private readonly DataSet ds = new DataSet();
        private User user;

        static AdminHomePage()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["piggyDB"].ConnectionString;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.UrlReferrer != null)
            {
                string previousPage = Request.UrlReferrer.Segments.Skip(1).Take(1).SingleOrDefault();
                if (previousPage == "Register.aspx")
                {
                    Response.Write("<script>alert('Registered successfully!');</script>");
                }
            }

            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                user = (User)Session["User"];
                if(user.isAdmin == false)
                {
                    Response.Redirect("Login.aspx");
                }
                header.Text = "Welcome " + user.userName + "!";
            }

            noNotifications.Visible = true;
            notificationPanel.Visible = false;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if(notificationGrid.Rows.Count != 0)
            {
                noNotifications.Visible = false;
                notificationPanel.Visible = true;
            }
        }

        private void updateApprovalStatus(string userId, string restaurantId, bool isApproved)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE Reviews SET isApproved = @isApproved WHERE UserId = @userId AND RestaurantId=@restaurantId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@restaurantId", restaurantId);

                    if(isApproved)
                    {
                        cmd.Parameters.AddWithValue("@isApproved", "1");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isApproved", "0");
                    }

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void notificationGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridView grid = sender as GridView;
            GridViewRow row = grid.Rows[index];
            HiddenField userId = (HiddenField)row.FindControl("userId");
            HiddenField restaurantId = (HiddenField)row.FindControl("restaurantId");

            if (e.CommandName == "ApproveComment")
            {
                updateApprovalStatus(userId.Value, restaurantId.Value, true);
            } 
            else if(e.CommandName == "RejectComment")
            {
                updateApprovalStatus(userId.Value, restaurantId.Value, false);
            }

            notificationGrid.DataBind();
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            string sqlQuery = "INSERT INTO Restaurants([Name], Location, Cuisine, Description) VALUES(@name, @location, @cuisine, @description)";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@location", location.Text);
                    cmd.Parameters.AddWithValue("@cuisine", cuisine.Text);
                    cmd.Parameters.AddWithValue("@description", description.Text);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            createRestaurant.Visible = false;
        }

        protected void createNewRestaurant_Click(object sender, EventArgs e)
        {
            noNotifications.Visible = false;
            createRestaurant.Visible = true;
        }

        protected void searchRestaurant_Click(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx");
        }

        protected void createNewAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}