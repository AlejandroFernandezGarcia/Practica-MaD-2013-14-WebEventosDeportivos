using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationService;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;

using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendation
{
    public partial class ViewRecomendations : System.Web.UI.Page
    {
        protected readonly IRecommendationService RecommendationService =
            UnityResolver.Resolve<IRecommendationService>();

        protected readonly IEventService EventService =
            UnityResolver.Resolve<IEventService>();

        protected readonly IUsersGroupService UsersGroupService = 
            UnityResolver.Resolve<IUsersGroupService>();

        private const int NUM_RECOMMENDATIONS_PER_PAGE = 10;

        protected int RecommendationListCurrentRow { get; set; }
        protected bool UserIsLogged { get; set; }
        protected long UserProfileId { get; set; }

        private bool morePages = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            linkNext.Visible = false;
            linkPrevius.Visible = false;

            String startIndexStr = Request.QueryString["startIndex"];

            if (startIndexStr == null)
            {
                ViewState["startIndex"] = 0;
            }
            else
            {
                ViewState["startIndex"] = Convert.ToInt16(startIndexStr);
            }

            // initialize UserIsLogged & UserProfileId
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                GoToAuthentication();
                return;
            }
            UserProfileId = userSession.UserProfileId;
            UserIsLogged = true;

            List<Model.Recommendation> listRecommendations =
                RecommendationService.FindRecommendationsReceivedByUserGroupOfUser
                (UserProfileId, Convert.ToInt32(ViewState["startIndex"].ToString()), 
                NUM_RECOMMENDATIONS_PER_PAGE + 1);

            if (listRecommendations.Count == 0)
            {
                lblEmptyList.Visible = true;
                linkNext.Visible = false;
                linkPrevius.Visible = false;
            }
            else
            {
                if (listRecommendations.Count == (NUM_RECOMMENDATIONS_PER_PAGE + 1))
                {
                    morePages = true;
                    listRecommendations.Remove(listRecommendations.Last());
                }
                PopulateRecommendationList(listRecommendations);
            }

            if (morePages)
            {
                linkNext.Visible = true;
                int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) + NUM_RECOMMENDATIONS_PER_PAGE;
                linkNext.NavigateUrl = "~/Pages/Recommendation/ViewRecomendations.aspx" + "?startIndex=" + startIndex;
                listRecommendations.Remove(listRecommendations.Last());
            }
            if (Convert.ToInt32(ViewState["startIndex"].ToString()) != 0)
            {
                linkPrevius.Visible = true;
                int startIndex = Convert.ToInt32(ViewState["startIndex"].ToString()) - NUM_RECOMMENDATIONS_PER_PAGE;
                linkPrevius.NavigateUrl = "~/Pages/Recommendation/ViewRecomendations.aspx" + "?startIndex=" + startIndex;
            }
        }

        protected void RecommendationList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // if the data bound item is an item or alternating item (not the header etc)
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Model.Recommendation item = (Model.Recommendation)e.Item.DataItem;
                RecommendationListCurrentRow++;
                if (RecommendationListCurrentRow % 2 == 0)
                {
                    TableRow tr = (TableRow)e.Item.FindControl("tr");
                    tr.CssClass = "alt";
                }

                HyperLink linkViewCommentButton = (HyperLink)e.Item.FindControl("linkViewComments");
                linkViewCommentButton.NavigateUrl = "~/Pages/Comment/ViewComments.aspx" + "?eventId=" + item.eventId.ToString();
                Model.Event evento = EventService.FindEventById(item.eventId);
                linkViewCommentButton.Text = evento.name;

                Label lclGroup = (Label)e.Item.FindControl("lblGroup");
                UsersGroup usersGroup = UsersGroupService.FindById(item.usersGroupId);
                lclGroup.Text = usersGroup.name;

                Label lclRecomText = (Label)e.Item.FindControl("lblRecomText");
                lclRecomText.Text = item.text;
            }
        }


        private void PopulateRecommendationList(List<Model.Recommendation> list)
        {
            RecommendationList.DataSource = list;
            RecommendationListCurrentRow = 0;
            RecommendationList.DataBind();
        }

        private void GoToAuthentication()
        {
            SessionManager.Logout(Context);
            Response.Redirect(Response.
                ApplyAppPathModifier("~/Pages/User/Authentication.aspx"
                + "?ReturnUrl=%2fPages%2fRecommendation%2fViewRecomendations.aspx"));
        }
    }
}