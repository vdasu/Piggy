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
    public partial class Search : Page
    {
        private static readonly string connectionString;
        private readonly DataSet ds = new DataSet();
        private User user;
        static Search()
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

                // fetch the cookie and views object

                string userViewCookieJson = Request.Cookies[this.user.userName].Value;
                Views userViews = new JavaScriptSerializer().Deserialize<Views>(userViewCookieJson);

                if (userViews.MaxViewedRestaurant == String.Empty)
                {
                    MostViewedPanel.Visible = false;
                }

                else
                {
                    // Get id and name from the retrieved string

                    string restaurantIdParam = userViews.MaxViewedRestaurant.Split('_')[0];
                    string restaurantNameParam = userViews.MaxViewedRestaurant.Split('_')[1];

                    // hlink to the details page

                    MostViewedHLink.Text = restaurantNameParam;
                    String.Format(MostViewedHLink.NavigateUrl, restaurantIdParam, restaurantNameParam);
                }
            }

            header.Text = "Welcome " + user.userName + "!";
        }
        protected void search_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                MostViewedPanel.Visible = false;
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