using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public partial class PracticaMaD : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                ContentPlaceHolder_MenuExplanation.Visible = false;
                lblDash2.Visible = false;
                lblDash3.Visible = false;
                lnkUpdate.Visible = false;
                lnkLogout.Visible = false;
            }
            else
            {
                ContentPlaceHolder_MenuWelcome.Visible = false;
                lblDash1.Visible = false;
                lnkAuthenticate.Visible = false;
            }
        }
    }
}