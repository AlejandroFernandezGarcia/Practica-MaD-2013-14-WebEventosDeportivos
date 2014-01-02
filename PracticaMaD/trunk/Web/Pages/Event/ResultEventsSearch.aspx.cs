using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
//TODO Falta paginar
namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Event
{
    public partial class ResultEventsSearch : System.Web.UI.Page
    {
        private readonly IEventService eventService =
           UnityResolver.Resolve<IEventService>();

        private int EventListCurrentRow { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            String keywords = Request.QueryString["keywords"];
            String finallyKeywords = keywords.Replace("+", " ");


            String category = Request.QueryString["category"];
            if (!Page.IsPostBack)
            {
                
                if (category == null)
                {
                    EventList.DataSource = eventService.FindByKeywords(keywords);
                }
                else
                {
                    long categoryId = Convert.ToInt64(category);

                    EventList.DataSource = eventService.FindByKeywords(finallyKeywords, categoryId);
                }
                EventListCurrentRow = 0;
                EventList.DataBind();
            }
        }

        protected void EventList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // if the data bound item is an item or alternating item (not the header etc)
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EventCategoryDto item = (EventCategoryDto)e.Item.DataItem;
                EventListCurrentRow++;
                if (EventListCurrentRow % 2 == 0)
                {
                    TableRow tr = (TableRow)e.Item.FindControl("tr");
                    tr.CssClass = "alt";
                }

                Label lblName = (Label)e.Item.FindControl("lblName");
                lblName.Text = item.evento.name;
                lblName.ToolTip = item.evento.description;

                //Todo Arreglar category
                Label lblCategory = (Label)e.Item.FindControl("lblCategory");
                lblCategory.Text = "categorua";//item.category.name;

                Label lblDate = (Label)e.Item.FindControl("lblDate");
                lblDate.Text = item.evento.date.ToString();
            }
        }
    }
}