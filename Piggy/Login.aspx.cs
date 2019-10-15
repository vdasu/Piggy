using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

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

        }

        protected void signup_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect("Register.aspx");
            }
        }

        protected void login_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT isAdmin FROM Users where UserName = @uname AND Password = @password COLLATE SQL_Latin1_General_CP1_CS_AS";
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
                                Response.Write("<script>alert('" + isAdmin + "');</script>");
                                User user = new User(username.Text, password.Text, bool.Parse(isAdmin));
                                Session["User"] = user;
                                ds.Clear();
                                
                                if (isAdmin == "True")
                                {
                                    Response.Redirect("AdminHomePage.aspx");
                                } 
                                else
                                {
                                    Response.Redirect("UserHomePage.aspx");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}