﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Piggy
{
    public partial class UserHomePage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"] == null)
            {
                Response.Write("<script>alert(User not found!);</script>");
                Response.Redirect("Login.aspx");
            }
        }

        protected void search_Click(object sender, EventArgs e)
        {

        }
    }
}