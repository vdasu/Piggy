﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Piggy
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void passwordMatchValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(password.Text != reenterPassword.Text)
            {
                args.IsValid = false;
            }
        }
    }
}