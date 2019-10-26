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
    public partial class UserHomePage : Page
    {
        private static readonly string connectionString;
        private readonly DataSet ds = new DataSet();
        private User user;
        static UserHomePage()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["piggyDB"].ConnectionString;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Site1 master = (Site1)this.Master;
            master.ShowLogout = true;

            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            user = (User)Session["User"];

            if (!IsPostBack)
            {
                searchStatus.Visible = false;
                search.Visible = false;
                searchResultsLabel.Visible = false;
                searchKey.Visible = false;
                searchKeyLabel.Visible = false;
            }

            header.Text = "Welcome " + user.userName + "!";
        }
        protected void search_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        if (searchCategoryList.SelectedItem.Value == "AvgRating")
                        {
                            cmd.CommandText = "SELECT * FROM Restaurants WHERE AvgRating >= @value";
                        }
                        else
                        {
                            cmd.CommandText = String.Format("SELECT * FROM Restaurants WHERE {0} = @value", searchCategoryList.SelectedItem.Value);
                        }                        
                        cmd.Parameters.AddWithValue("@value", searchKey.Text);
                        searchKey.Text = "";
                        cmd.Connection = conn;

                        using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            adapter.Fill(ds, "restaurants");

                            if(ds.Tables["restaurants"].Rows.Count==0)
                            {
                                searchGridView.Visible = false;
                                searchStatus.Visible = true;
                            } else
                            {
                                searchGridView.Visible = true;
                                searchStatus.Visible = false;
                                searchGridView.DataSource = ds.Tables["restaurants"];
                                searchGridView.DataBind();
                            }

                            ds.Clear();
                        }
                    }
                }
            }
        }

        protected void searchCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchKeyLabel.Visible = true;
            searchKey.Visible = true;
            search.Visible = true;

            searchKeyLabel.Text = "Enter " + searchCategoryList.SelectedItem.Text + ": ";
        }
    }
}