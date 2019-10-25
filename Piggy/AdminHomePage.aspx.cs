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
            Site1 master = (Site1)this.Master;
            master.ShowLogout = true;

            if (Session["User"] == null)
            {
                Response.Write("<script>alert(Session Expired, login again.);</script>");
                Response.Redirect("Login.aspx");
            }

            adminLanding.Visible = true;
            notificationPanel.Visible = false;

            user = (User)Session["User"];
            header.Text = "Welcome " + user.userName + "!";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if(notificationGrid.Rows.Count != 0)
            {
                adminLanding.Visible = false;
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
                ApprovalStatus.Text = "Row " + (index + 1).ToString() + " approved";
                ApprovalStatus.Text += "Callbackx1";
            } 
            else if(e.CommandName == "RejectComment")
            {
                updateApprovalStatus(userId.Value, restaurantId.Value, false);
                ApprovalStatus.Text = "Row " + (index + 1).ToString() + " rejected";
                ApprovalStatus.Text += "Callbackx2";
            }

            notificationGrid.DataBind();
        }
    }
}