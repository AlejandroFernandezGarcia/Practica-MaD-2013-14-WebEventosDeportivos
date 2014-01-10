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

        protected int RecommendationListCurrentRow { get; set; }
        protected bool UserIsLogged { get; set; }
        protected long UserProfileId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // initialize UserIsLogged & UserProfileId
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                GoToAuthentication();
                return;
            }
            UserProfileId = userSession.UserProfileId;
            UserIsLogged = true;

            // populate RecommendationList repeater
            if (!Page.IsPostBack)
            {
                PopulateRecommendationList();
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
                //String eId = item.eventId.ToString();
                linkViewCommentButton.Text = evento.name;

                Label lclGroup = (Label)e.Item.FindControl("lblGroup");
                UsersGroup usersGroup = UsersGroupService.FindById(item.usersGroupId);
                lclGroup.Text = usersGroup.name;

                Label lclRecomText = (Label)e.Item.FindControl("lblRecomText");
                lclRecomText.Text = item.text;
            }
        }


        private void PopulateRecommendationList()
        {
            RecommendationList.DataSource =
                RecommendationService.FindRecommendationsReceivedByUserGroupOfUser(UserProfileId);
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