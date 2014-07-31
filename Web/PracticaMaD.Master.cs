using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public partial class PracticaMaD : System.Web.UI.MasterPage
    {
        private readonly ITagService tagService =
           UnityResolver.Resolve<ITagService>();

        private const int MAX_TAGS_FOR_LINE = 8;

        private double MAX_PERCENT = 0D;

        private int currentTagsForLine = 0;

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

            List<TagDto> listOfTagDtos = tagService.FindTagsPercent();
            searchMaxPercent(listOfTagDtos);

            foreach (TagDto tagDto in listOfTagDtos)
            {
                currentTagsForLine++;
                HyperLink hl = new HyperLink();
                hl.Text = tagDto.tag.tagName;
                hl.NavigateUrl = "~/Pages/Comment/ViewComments.aspx" + "?cloudTag=" + tagDto.tag.id;
                asignateCss(hl, tagDto.percent);

                ContentPlaceHolder_CloudOfTags.Controls.Add(hl);
                if ((listOfTagDtos.Last() != tagDto) && (currentTagsForLine != MAX_TAGS_FOR_LINE))
                {
                    Label lbl = new Label();
                    lbl.Text = "  |  ";
                    ContentPlaceHolder_CloudOfTags.Controls.Add(lbl);
                }
                else if (currentTagsForLine == MAX_TAGS_FOR_LINE)
                {
                    ContentPlaceHolder_CloudOfTags.Controls.Add(new LiteralControl("<br/>"));
                    currentTagsForLine = 0;
                }
            }

        }

        /// <summary>
        /// Asignates the CSS to HyperLinks.
        /// </summary>
        /// <param name="hyperlink">The hyperlink.</param>
        /// <param name="percent">The percent.</param>
        private void asignateCss(HyperLink hyperlink, double percent)
        {
            //Para realizar una mejor distribución, se considera el
            //porcentaje del tag que más sale como el 100%. Por lo tanto
            //al dividir por el maximo porcentaje y multiplicar por 100 
            //cualquier porcentaje entre el máximo da un número mayor 
            //y se reparte mejor ya que hay mayores diferencias entre los porcentajes.
            //De no hacer esto todos los porcentajes estarian por debajo
            //del 30% y no se reflejaria practicamente la diferencia de apariciones
            //en la nube de tags.
            percent = (percent/MAX_PERCENT)*100;

            if (percent > 90D)
            {
                hyperlink.CssClass = "FontSize90-100";
            }else if(percent > 80D)
            {
                hyperlink.CssClass = "FontSize80-90";
            }else if (percent > 70D)
            {
                hyperlink.CssClass = "FontSize70-80";
            }else if (percent > 60D)
            {
                hyperlink.CssClass = "FontSize60-70";
            }else if (percent > 55D)
            {
                hyperlink.CssClass = "FontSize55-60";
            }else if (percent > 50D)
            {
                hyperlink.CssClass = "FontSize50-55";
            }else if (percent > 45D)
            {
                hyperlink.CssClass = "FontSize45-50";
            }else if (percent > 40D)
            {
                hyperlink.CssClass = "FontSize40-45";
            }else if (percent > 35D)
            {
                hyperlink.CssClass = "FontSize35-40";
            }else if (percent > 30D)
            {
                hyperlink.CssClass = "FontSize30-35";
            }else if (percent > 25D)
            {
                hyperlink.CssClass = "FontSize25-30";
            }else if (percent > 20D)
            {
                hyperlink.CssClass = "FontSize20-25";
            }else if (percent > 15D)
            {
                hyperlink.CssClass = "FontSize15-20";
            }else if (percent > 10D)
            {
                hyperlink.CssClass = "FontSize10-15";
            }else if (percent > 5D)
            {
                hyperlink.CssClass = "FontSize5-10";
            }else if (percent > 0D)
            {
                hyperlink.CssClass = "FontSize0-5";
            }
        }

        /// <summary>
        /// Searches the maximum percent in tagDto list.
        /// </summary>
        /// <param name="list">The list.</param>
        private void searchMaxPercent(List<TagDto> list)
        {
            //Busca el porcentaje, del tag que más se repite

            double currentPercent = 0D;
            foreach (TagDto t in list)
            {
                if (t.percent > currentPercent)
                {
                    currentPercent = t.percent;
                }
            }

            MAX_PERCENT = currentPercent;
        }
    }
}