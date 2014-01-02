using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendation
{
    public partial class NewRecommendation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //recibe el parametro eventId
            String eventId = Request.QueryString["eventId"];
        }
    }
}