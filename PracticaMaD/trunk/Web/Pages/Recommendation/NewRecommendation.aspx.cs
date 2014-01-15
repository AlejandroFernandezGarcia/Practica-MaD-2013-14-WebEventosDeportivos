using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

//UNDONE Redimensionar el textBox de la recomendation
namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Recommendation
{
    public partial class NewRecommendation : System.Web.UI.Page
    {
        protected readonly IUsersGroupService UsersGroupService =
            UnityResolver.Resolve<IUsersGroupService>();
        protected readonly IRecommendationService RecommendationService =
            UnityResolver.Resolve<IRecommendationService>();
        protected readonly IEventService EventService =
            UnityResolver.Resolve<IEventService>();

        protected int GroupListCurrentRow { get; set; }
        protected bool UserIsLogged { get; set; }
        protected long UserProfileId { get; set; }
        protected long EventId { get; set; }

        protected int RecommendationListCurrentRow { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //recibe el parametro eventId
            String passedEvent = Request.QueryString["eventId"];
            //long eventId = Convert.ToInt64(passedEvent);
            EventId = Convert.ToInt64(passedEvent);

            // initialize UserIsLogged & UserProfileId
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                GoToAuthentication();
                return;
            }
            UserProfileId = userSession.UserProfileId;
            UserIsLogged = true;

            // hide labels
            lblOperationSucceed.Visible = false;

            try
            {
                Model.Event evento = EventService.FindEventById(EventId);
                lclEventNameExt.Text = evento.name;

            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
            }

            // populate GroupList repeater
            if (!Page.IsPostBack)
            {
                PopulateGroupList();
            }
        }

        protected void GroupList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // if the data bound item is an item or alternating item (not the header etc)
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                UsersGroupDto item = (UsersGroupDto)e.Item.DataItem;
                GroupListCurrentRow++;
                if (GroupListCurrentRow % 2 == 0)
                {
                    TableRow tr = (TableRow)e.Item.FindControl("tr");
                    tr.CssClass = "alt";
                }

                Label lblName = (Label)e.Item.FindControl("lblName");
                lblName.Text = item.usersGroup.name;

                CheckBox cbRecommend = (CheckBox)e.Item.FindControl("cbRecommend");
                cbRecommend.Checked = false;

                HiddenField hfId = (HiddenField)e.Item.FindControl("hfId");
                hfId.Value = item.usersGroup.id.ToString();
            }
        }

        protected void BtnRecommendClick(object sender, EventArgs e)
        {
            lblNoGroupSelected.Visible = false;
            lblOperationSucceed.Visible = false;
            lblMaxRecomendationLength.Visible = false;

            List<long> usersGroupIds = new List<long>();
            List<RepeaterItem> addedItems = new List<RepeaterItem>();

            foreach (RepeaterItem item in GroupList.Items)
            {
                if (item.ItemType == ListItemType.Item
                    || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox cbRecommend = (CheckBox)item.FindControl("cbRecommend");
                    if (cbRecommend.Checked && cbRecommend.Enabled)
                    {
                        // remember id (to use the service later)
                        HiddenField hfId = (HiddenField)item.FindControl("hfId");
                        long id = Int64.Parse(hfId.Value);
                        usersGroupIds.Add(id);

                        // remember item (to update view after service use)
                        addedItems.Add(item);
                    }
                }
            }
            txtRecommendation.MaxLength = 1000;
            if (txtRecommendation.Text.Length >= 1000)
            {
                lblMaxRecomendationLength.Visible = true;
            }
            else
            {
                if (usersGroupIds.Count > 0)
                {
                    try
                    {
                        RecommendationService.Create(EventId, usersGroupIds, txtRecommendation.Text,
                            SessionManager.GetUserSession(Context).UserProfileId);

                        // show success feedback
                        lblOperationSucceed.Visible = true;

                        // update view
                        foreach (RepeaterItem item in addedItems)
                        {
                            // disable checkbox
                            ((CheckBox) item.FindControl("cbRecommend")).Enabled = false;
                        }

                        // update view
                        PopulateGroupList();

                        txtRecommendation.Text = "";
                    }
                    catch (NullReferenceException)
                    {
                        // this happens when SessionManager.GetUserSession returns null,
                        // so we force logout and redirect to the authentication page
                        SessionManager.Logout(Context);
                        Response.Redirect(Response.
                            ApplyAppPathModifier("~/Pages/User/Authentication.aspx"
                                                 + "?ReturnUrl=%2fPages%2fRecommend%2fNewRecommendation.aspx"));
                    }
                }
                else
                {
                    lblNoGroupSelected.Visible = true;
                }
            }

        }

        private void PopulateGroupList()
        {
            GroupList.DataSource = UsersGroupService.FindByUserId(UserProfileId);
            GroupListCurrentRow = 0;
            GroupList.DataBind();
        }

        private void GoToAuthentication()
        {
            SessionManager.Logout(Context);
            Response.Redirect(Response.
                ApplyAppPathModifier("~/Pages/User/Authentication.aspx"
                + "?ReturnUrl=%2fPages%2fRecommendation%2fNewRecommendation.aspx"));
        }

    }
}