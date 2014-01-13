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
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Group
{
    public partial class MyGroups : SpecificCulturePage
    {
        protected readonly IUsersGroupService UsersGroupService =
            UnityResolver.Resolve<IUsersGroupService>();

        protected int GroupListCurrentRow { get; set; }
        protected bool UserIsLogged { get; set; }
        protected long UserProfileId { get; set; }

        private int _currentPage;
        public static readonly int GROUPS_PER_PAGE = 10;

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

            // hide/show labels
            lblOperationSucceed.Visible = ParseInt(Request.QueryString["success"]) == 1;
            lblOperationFailed.Visible = false;

            // last page
            float count = (float)UsersGroupService.FindByUserId(UserProfileId).Count;
            float perPage = (float)GROUPS_PER_PAGE;
            int lastPage = (int)Math.Ceiling(count / perPage);
            Paginator.LastPage = lastPage;

            // current page
            _currentPage = 1;
            if (Request.QueryString["page"] != null)
            {
                _currentPage = ParseInt(Request.QueryString["page"]);
                if (_currentPage > lastPage)
                {
                    _currentPage = lastPage;
                }
                else if (_currentPage < 1)
                {
                    _currentPage = 1;
                }
            }
            Paginator.CurrentPage = _currentPage;

            // populate GroupList repeater
            if (!Page.IsPostBack)
            {
                // my groups
                var usersGroups = UsersGroupService.FindByUserId(UserProfileId,
                    (_currentPage - 1) * GROUPS_PER_PAGE, GROUPS_PER_PAGE);
                GroupList.DataSource = usersGroups;
                GroupListCurrentRow = 0;
                GroupList.DataBind();
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

                Label lblRecomCount = (Label)e.Item.FindControl("lblRecomCount");
                lblRecomCount.Text = item.numOfRecommendations.ToString();

                Label lblUserCount = (Label)e.Item.FindControl("lblUserCount");
                lblUserCount.Text = item.numOfUsers.ToString();

                CheckBox cbLeave = (CheckBox)e.Item.FindControl("cbLeave");
                cbLeave.Checked = false;

                HiddenField hfId = (HiddenField)e.Item.FindControl("hfId");
                hfId.Value = item.usersGroup.id.ToString();
            }
        }

        protected void BtnLeaveGroupsClick(object sender, EventArgs e)
        {
            List<long> usersGroupIds = new List<long>();

            foreach (RepeaterItem item in GroupList.Items)
            {
                if (item.ItemType == ListItemType.Item
                    || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox cbLeave = (CheckBox)item.FindControl("cbLeave");
                    if (cbLeave.Checked)
                    {
                        // remember id (to use the service later)
                        HiddenField hfId = (HiddenField)item.FindControl("hfId");
                        long id = Int64.Parse(hfId.Value);
                        usersGroupIds.Add(id);
                    }
                }
            }

            if (usersGroupIds.Count > 0)
            {
                try
                {
                    // use service to add the user to the groups
                    UsersGroupService.RemoveUserFromGroup(usersGroupIds, UserProfileId);

                    // update view
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Group/MyGroups.aspx"
                        + "?page=" + _currentPage + "&success=1"));
                }
                catch (Exception ex)
                {
                    if (ex is DuplicateInstanceException || ex is UpdateException || ex is SqlException)
                    {
                        lblOperationFailed.Visible = true;
                        return;
                    }
                    throw;
                }
            }
        }

        private void PopulateGroupList()
        {
            // last page
            float count = (float)UsersGroupService.FindByUserId(UserProfileId).Count;
            float perPage = (float)GROUPS_PER_PAGE;
            int lastPage = (int)Math.Ceiling(count / perPage);
            Paginator.LastPage = lastPage;

            // current page
            int currentPage = 1;
            if (Request.QueryString["page"] != null)
            {
                currentPage = ParseInt(Request.QueryString["page"]);
                if (currentPage > lastPage)
                {
                    currentPage = lastPage;
                }
                else if (currentPage < 1)
                {
                    currentPage = 1;
                }
            }
            Paginator.CurrentPage = currentPage;

            // my groups
            var usersGroups = UsersGroupService.FindByUserId(UserProfileId,
                (currentPage - 1) * GROUPS_PER_PAGE, GROUPS_PER_PAGE);
            GroupList.DataSource = usersGroups;
            GroupListCurrentRow = 0;
            GroupList.DataBind();
        }

        private void GoToAuthentication()
        {
            SessionManager.Logout(Context);
            Response.Redirect(Response.
                ApplyAppPathModifier("~/Pages/User/Authentication.aspx"
                + "?ReturnUrl=%2fPages%2fGroup%2fMyGroups.aspx"));
        }

        private int ParseInt(String str)
        {
            try
            {
                return Int32.Parse(str);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}