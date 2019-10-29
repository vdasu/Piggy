using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;

namespace Piggy
{
    public partial class Login : Page
    {
        private static readonly string connectionString;
        private readonly DataSet ds = new DataSet();

        static Login()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["piggyDB"].ConnectionString;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Piggy master = (Piggy)this.Master;
            master.ShowLogout = false;

            if (IsPostBack) return;
            if (Request.UrlReferrer != null) 
            {
                string previousPage = Request.UrlReferrer.Segments.Skip(1).Take(1).SingleOrDefault();
                if (previousPage == "Register.aspx")
                {
                    Response.Write("<script>alert('Registered successfully!');</script>");
                }  else if(previousPage == "AdminHomePage.aspx" || previousPage == "Search.aspx")
                {
                    Response.Write("<script>alert(Session Expired, login again.);</script>");
                }
            }
        }

        protected void signup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void login_Click(object sender, EventArgs e)
        {
            string sqlQuery = "SELECT Id, isAdmin FROM Users where UserName = @uname AND Password = @password COLLATE SQL_Latin1_General_CP1_CS_AS";
            if (Page.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = sqlQuery;
                        cmd.Parameters.AddWithValue("@uname", username.Text);
                        cmd.Parameters.AddWithValue("@password", password.Text);
                        cmd.Connection = conn;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            adapter.Fill(ds, "user");

                            if (ds.Tables["user"].Rows.Count == 0)
                            {
                                Response.Write("<script>alert('User not found. Invalid credentials');</script>");
                            }
                            else
                            {
                                string isAdmin = ds.Tables["user"].Rows[0]["isAdmin"].ToString();
                                string userId = ds.Tables["user"].Rows[0]["Id"].ToString();
                                User user = new User(int.Parse(userId), username.Text, password.Text, bool.Parse(isAdmin));
                                Session["User"] = user;

                                if (Request.Cookies[user.userName] == null)
                                {
                                    HttpCookie cookie = new HttpCookie(user.userName);
                                    cookie.Expires = DateTime.Now.AddMinutes(20);
                                    string userViewsJson = new JavaScriptSerializer().Serialize(new Views());
                                    cookie.Value = userViewsJson;
                                    Response.Cookies.Add(cookie);
                                }
                                ds.Clear();
                                
                                if (isAdmin == "True")
                                {
                                    Response.Redirect("AdminHomePage.aspx");
                                } 
                                else
                                {
                                    Response.Redirect("Search.aspx");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}