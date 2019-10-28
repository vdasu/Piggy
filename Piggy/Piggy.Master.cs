using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Piggy
{
    public partial class Piggy : System.Web.UI.MasterPage
    {
        private bool showLogout = true;
        public bool ShowLogout
        {
            get
            {
                return showLogout;
            }

            set
            {
                showLogout = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            logout.Visible = showLogout;
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Session["newReview"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}