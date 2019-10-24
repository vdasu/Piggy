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

            notificationPanel.Visible = false;

            user = (User)Session["User"];
            header.Text = "Welcome " + user.userName + "!";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if(notificationGrid.Rows.Count != 0)
            {
                notificationPanel.Visible = true;
            }
        }

        protected void notificationGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow row = null;
            GridView grid = sender as GridView;

            if(e.CommandName == "ApproveComment")
            {
                index = Convert.ToInt32(e.CommandArgument);
                row = grid.Rows[index];
                ApprovalStatus.Text = "Row " + (index+1).ToString() + " approved";
            } 
            else if(e.CommandName == "RejectComment")
            {
                index = Convert.ToInt32(e.CommandArgument);
                row = grid.Rows[index];
                ApprovalStatus.Text = "Row " + (index+1).ToString() + " rejected";
            }
        }
    }
}