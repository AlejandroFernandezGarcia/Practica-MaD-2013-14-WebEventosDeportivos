using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Event
{
    public partial class SearchEvents : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lblKeywordsError.Visible = false;
        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            lblKeywordsError.Visible = false;

            if (Page.IsValid)
            {
                String keywords = txtKeywords.Text;
                if (keywords.Count() != 0)
                {
                    int selectedElement = CategoryDropDownList.SelectedIndex;

                    if (selectedElement == 0)
                    {
                        Response.Redirect(Response.ApplyAppPathModifier("./ResultEventsSearch.aspx" 
                            + "?keywords=" + keywords.Replace(" ", "+")));
                    }
                    else
                    {
                        Response.Redirect(Response.ApplyAppPathModifier("./ResultEventsSearch.aspx" + "?keywords="
                                                      + keywords.Replace(" ", "+") + "&category=" + selectedElement));
                    }
                }
                else
                {
                    lblKeywordsError.Visible = true;
                }

            }
        }
    }
}