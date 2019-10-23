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
                     

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            Response.Write("<script>alert('Registration completed!');</script>");
                            Response.Redirect("Login.aspx");
                        } 
                        catch (Exception exp)
                        {
                            Response.Write("<script>alert('Oops! Something went wrong. Please try again...');</script>");
                            Console.WriteLine(exp.ToString());
                        }
                        finally
                        {
                            conn.Close();
                        }
                    
                    }
                }
            }
        }
    }
}