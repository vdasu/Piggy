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
        static string connectionString;
        DataSet ds = new DataSet();

        public static void MessageBox(Page page, string message)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + message + "')alert", true);
        }

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
                        cmd.CommandText = "SELECT isAdmin FROM Users where UserName = @uname AND Password = @password";
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