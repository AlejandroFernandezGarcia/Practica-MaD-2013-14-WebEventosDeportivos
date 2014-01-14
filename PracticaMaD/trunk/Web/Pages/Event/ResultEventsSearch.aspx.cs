using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Event
{
    public partial class ResultEventsSearch : System.Web.UI.Page
    {
        private readonly IEventService eventService =
           UnityResolver.Resolve<IEventService>();

        private const int NUM_EVENTS_PER_PAGE = 10;

        private int EventListCurrentRow { get; set; }

        private bool morePages = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            linkNext.Visible = false;
            linkPrevius.Visible = false;

            String keywords = Request.QueryString["keywords"];
            String finallyKeywords = keywords.Replace("+", " ");

            List<EventCategoryDto> listEvents = new List<EventCategoryDto>();

            String addCommentOk = Request.QueryString["commentAdd"];
            if (addCommentOk != null)
            {
                lblAddComment.Visible = true;
            }

            String startIndexStr = Request.QueryString["startIndex"];

            if (startIndexStr == null)
            {
                ViewState["startIndex"] = 0;
            }
            else
            {
                ViewState["startIndex"] = Convert.ToInt16(startIndexStr);
            }

            String category = Request.QueryString["category"];
            if (category == null)
            {
                listEvents = eventService.FindByKeywords(keywords, 
                    Convert.ToInt32(ViewState["startIndex"].ToString()),
                    NUM_EVENTS_PER_PAGE + 1);
            }
            else
            {
                long categoryId = Convert.ToInt64(category);

                listEvents = eventService.FindByKeywords(finallyKeywords, categoryId, 
                    Convert.ToInt32(ViewState["startIndex"].ToString()),
                    NUM_EVENTS_PER_PAGE + 1);
            }
            if (listEvents.Count == 0)
            {
                lblEmptyList.Visible = true;
            }
            else
            {
                if (listEvents.Count == (NUM_EVENTS_PER_PAGE + 1))
                {
                    morePages = true;
                    listEvents.Remove(listEvents.Last());
                }
                populateItems(listEvents);
            }
            
            if (morePages)
            {
                linkNext.Visible = true;
                int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) + NUM_EVENTS_PER_PAGE;
                String url = "~/Pages/Event/ResultEventsSearch.aspx" + "?keywords=" + keywords;
                if (category != null)
                {
                    url += "&category=" + category;
                }
                url += "&startIndex=" + startIndex;
                linkNext.PostBackUrl = url;
                
            }
            if (Convert.ToInt32(ViewState["startIndex"].ToString()) != 0)
            {
                linkPrevius.Visible = true;
                int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) - NUM_EVENTS_PER_PAGE;
                String url = "~/Pages/Event/ResultEventsSearch.aspx" + "?keywords=" + keywords;
                if (category != null)
                {
                    url += "&category=" + category;
                }
                url += "&startIndex=" + startIndex;
                linkPrevius.PostBackUrl = url;
            }
        }

        private void populateItems(List<EventCategoryDto> list)
        {
            EventList.DataSource = list;
            EventListCurrentRow = 0;
            EventList.DataBind();
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

                Label lblCategory = (Label)e.Item.FindControl("lblCategory");
                lblCategory.Text = item.category.name;

                Label lblDate = (Label)e.Item.FindControl("lblDate");
                lblDate.Text = item.evento.date.ToString();

                HyperLink linkAddCommentButton = (HyperLink)e.Item.FindControl("linkAddComment");
                linkAddCommentButton.NavigateUrl = "~/Pages/Comment/AddComment.aspx" + "?eventId=" +item.evento.id;

                HyperLink linkViewCommentButton = (HyperLink)e.Item.FindControl("linkViewComments");
                linkViewCommentButton.NavigateUrl = "~/Pages/Comment/ViewComments.aspx" + "?eventId=" + item.evento.id;

                HyperLink linkRecommend = (HyperLink)e.Item.FindControl("linkRecommend");
                linkRecommend.NavigateUrl = "~/Pages/Recommendation/NewRecommendation.aspx" + "?eventId=" + item.evento.id;
            }
        }
    }
}