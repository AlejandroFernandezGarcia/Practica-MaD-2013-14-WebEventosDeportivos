using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
//TODO Paginar
using Es.Udc.DotNet.PracticaMaD.Model.UserService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class ViewComments : System.Web.UI.Page
    {
        private int CommentListCurrentRow { get; set; }

        private const int NUM_COMMENTS_PER_PAGE = 10;

        private readonly IEventService eventService =
          UnityResolver.Resolve<IEventService>();

        private readonly IUserService userService =
          UnityResolver.Resolve<IUserService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            linkNext.Visible = false;
            linkPrevius.Visible = false;

            String eventIdStr = Request.QueryString["eventId"];

            long eventId = Convert.ToInt64(eventIdStr);

            String startIndexStr = Request.QueryString["startIndex"];

            if (startIndexStr == null)
            {
                ViewState["startIndex"] = 0;
            }
            else
            {
                ViewState["startIndex"] = Convert.ToInt16(startIndexStr);
            }

            //aunque se muestren 10 recupero 11 para saber si va a haber siguiente.
            List<Model.Comment>  listComments = eventService.FindCommentsForEvent(eventId, 
                    Convert.ToInt32(ViewState["startIndex"].ToString()),
                    NUM_COMMENTS_PER_PAGE + 1);


            try
            {
                lclEventNameExt.Text = eventService.FindEventById(eventId).name;
                if (listComments.Count == 0)
                {
                    lblEmptyList.Visible = true;
                    linkNext.Visible = false;
                    linkPrevius.Visible = false;
                }
                else
                {
                    populateItems(listComments);
                }
            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
            }

            if (listComments.Count == (NUM_COMMENTS_PER_PAGE + 1))
            {
                linkNext.Visible = true;
                int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) + NUM_COMMENTS_PER_PAGE;
                linkNext.PostBackUrl = "~/Pages/Comment/ViewComments.aspx" + "?eventId=" + eventId +
                                       "&startIndex=" + startIndex;
            }
            if(Convert.ToInt32(ViewState["startIndex"].ToString()) != 0)
            {
                linkPrevius.Visible = true;
                int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) - NUM_COMMENTS_PER_PAGE;
                linkPrevius.PostBackUrl = "~/Pages/Comment/ViewComments.aspx" + "?eventId=" + eventId +
                                       "&startIndex=" + startIndex;
            }

        }

        private void populateItems(List<Model.Comment> list)
        {
            CommentList.DataSource = list;
            CommentListCurrentRow = 0;
            CommentList.DataBind();
        }

        protected void CommentList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Model.Comment item = (Model.Comment)e.Item.DataItem;
                CommentListCurrentRow++;
                if (CommentListCurrentRow % 2 == 0)
                {
                    TableRow tr = (TableRow)e.Item.FindControl("tr");
                    tr.CssClass = "alt";
                }

                Label lblUserName = (Label)e.Item.FindControl("lblUserName");
                UserProfileDetails user = userService.FindUserProfileDetails(item.userProfileId);
                lblUserName.Text = user.FirstName + " "+user.Surname;//peta seguro

                Label lblDate = (Label)e.Item.FindControl("lblDate");
                lblDate.Text = item.date.ToString();

                HyperLink linkToComment = (HyperLink)e.Item.FindControl("linkToComment");
                int lengthComment = 77;
                if (item.text.Length < lengthComment)
                {
                    lengthComment = item.text.Length;
                }
                linkToComment.Text = item.text.Substring(0, lengthComment) + " ...";
                linkToComment.NavigateUrl = "~/Pages/Comment/ViewCommentAndTag.aspx" + "?commentId=" + item.id;
            }
        }

        /*protected void linkPrevius_OnClick(object sender, EventArgs e)
        {
            ViewState["startIndex"] = Convert.ToInt32(ViewState["startIndex"].ToString()) - NUM_COMMENTS_PER_PAGE;

        }

        protected void linkNext_OnClick(object sender, EventArgs e)
        {
            ViewState["startIndex"] = Convert.ToInt32(ViewState["startIndex"].ToString()) + NUM_COMMENTS_PER_PAGE;
        }*/
    }
}