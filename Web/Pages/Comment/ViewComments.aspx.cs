using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class ViewComments : SpecificCulturePage
    {
        private int CommentListCurrentRow { get; set; }

        private const int NUM_COMMENTS_PER_PAGE = 10;

        private readonly IEventService eventService =
          UnityResolver.Resolve<IEventService>();

        private readonly ITagService tagService =
          UnityResolver.Resolve<ITagService>();

        private readonly IUserService userService =
          UnityResolver.Resolve<IUserService>();

        private bool morePages = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            linkNext.Visible = false;
            linkPrevius.Visible = false;

            String vComTag = Request.QueryString["vComTag"];
            if (vComTag != null)
            {
                if (vComTag.Equals("edited"))
                {
                    lblCommendEdited.Visible = true;
                }
                if (vComTag.Equals("deleted"))
                {
                    lblCommentDeleted.Visible = true;
                }
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

            List<Model.Comment> listComments;
            long tagId = -1;
            long eventId = -1;

            String cloudTag = Request.QueryString["cloudTag"];
            if (cloudTag != null)
            {

                #region For view comments for a tag.

                tagId = Convert.ToInt64(cloudTag.ToString());

                listComments = tagService.FindCommentsByTag(tagId, 
                    Convert.ToInt32(ViewState["startIndex"].ToString()),
                    NUM_COMMENTS_PER_PAGE + 1);

                try
                {
                    lclTagName.Visible = true;
                    lclEventNameExt.Text = tagService.FindTagById(tagId).tagName;
                }
                catch (InstanceNotFoundException)
                {
                    Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
                }
                #endregion

            }
            else
            {
                #region For view comments for event.

                String eventIdStr = Request.QueryString["eventId"];

                eventId = Convert.ToInt64(eventIdStr);

                //aunque se muestren 10 recupero 11 para saber si va a haber siguiente.
                listComments = eventService.FindCommentsForEvent(eventId,
                    Convert.ToInt32(ViewState["startIndex"].ToString()),
                    NUM_COMMENTS_PER_PAGE + 1);

                try
                {
                    lclEventName.Visible = true;
                    lclEventNameExt.Text = eventService.FindEventById(eventId).name;//dividir
                }
                catch (InstanceNotFoundException)
                {
                    Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
                }
                #endregion
            }

            if (listComments.Count == 0)
            {
                lblEmptyList.Visible = true;
                linkNext.Visible = false;
                linkPrevius.Visible = false;
            }
            else
            {
                if (listComments.Count == (NUM_COMMENTS_PER_PAGE + 1))
                {
                    morePages = true;
                    listComments.Remove(listComments.Last());
                }
                populateItems(listComments);
            }

            if (cloudTag != null)
            {
                #region For view comments for a tag.
                if (morePages)
                {
                    linkNext.Visible = true;
                    int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) + NUM_COMMENTS_PER_PAGE;
                    linkNext.NavigateUrl = "~/Pages/Comment/ViewComments.aspx" + "?cloudTag=" + tagId +
                                            "&startIndex=" + startIndex;
                }
                if (Convert.ToInt32(ViewState["startIndex"].ToString()) != 0)
                {
                    linkPrevius.Visible = true;
                    int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) - NUM_COMMENTS_PER_PAGE;
                    linkPrevius.NavigateUrl = "~/Pages/Comment/ViewComments.aspx" + "?cloudTag=" + tagId +
                                                "&startIndex=" + startIndex;
                }
                #endregion
            }
            else
            {
                #region For view comments for a event.
                if (morePages)
                {
                    linkNext.Visible = true;
                    int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) + NUM_COMMENTS_PER_PAGE;
                    linkNext.NavigateUrl = "~/Pages/Comment/ViewComments.aspx" + "?eventId=" + eventId +
                                            "&startIndex=" + startIndex;
                }
                if (Convert.ToInt32(ViewState["startIndex"].ToString()) != 0)
                {
                    linkPrevius.Visible = true;
                    int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) - NUM_COMMENTS_PER_PAGE;
                    linkPrevius.NavigateUrl = "~/Pages/Comment/ViewComments.aspx" + "?eventId=" + eventId +
                                                "&startIndex=" + startIndex;
                }
                #endregion
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
                lblUserName.Text = user.FirstName + " "+user.Surname;

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

    }
}