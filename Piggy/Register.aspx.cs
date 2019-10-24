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
    public partial class Register : Page
    {
        private static readonly string connectionString;

        static Register()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["piggyDB"].ConnectionString;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Site1 master = (Site1)this.Master;
            master.ShowLogout = false;

            if (IsPostBack) return;
        }

        protected void passwordMatchValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(password.Text != reenterPassword.Text)
            {
                args.IsValid = false;
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string sqlQuery = "INSERT INTO [Users](FName, LName, UserName, Password) values (@FName, @LName, @UserName, @Password)";

            if (Page.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = sqlQuery;
                        cmd.Parameters.AddWithValue("@FName", firstName.Text);
                        cmd.Parameters.AddWithValue("@LName", lastName.Text);
                        cmd.Parameters.AddWithValue("@UserName", username.Text);
                        cmd.Parameters.AddWithValue("@Password", password.Text);

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Login.aspx");
                    }
                }
            }
        }
    }
}