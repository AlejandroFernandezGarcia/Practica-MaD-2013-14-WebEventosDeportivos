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
    public partial class ViewGroups : SpecificCulturePage
    {
        protected readonly IUsersGroupService UsersGroupService =
            UnityResolver.Resolve<IUsersGroupService>();

        protected List<long> MyGroupIdList { get; set; }

        protected int GroupListCurrentRow { get; set; }
        protected bool UserIsLogged { get; set; }
        protected long UserProfileId { get; set; }

        public const int GROUPS_PER_PAGE = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            // initialize UserIsLogged & UserProfileId
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                UserProfileId = -1L;
                UserIsLogged = false;
            }
            else
            {
                UserProfileId = userSession.UserProfileId;
                UserIsLogged = true;
            }

            // hide labels
            lblUserAdded.Visible = false;
            lblOperationFailed.Visible = false;

            // populate GroupList repeater and Paginator
            if (!Page.IsPostBack)
            {
                PopulateGroupList();
            }
        }

        private void PopulateGroupList()
        {
            // paginator
            Paginator.ItemCount = UsersGroupService.FindAllGroups().Count;
            Paginator.ItemsPerPage = GROUPS_PER_PAGE;
            Paginator.UpdateView();

            // user groups
            MyGroupIdList = UserIsLogged ? GroupDtoListToIdList(UsersGroupService.FindByUserId(
                UserProfileId)) : new List<long>();

            // all groups
            var usersGroups = UsersGroupService.FindAllGroups(
                Paginator.StartIndex, Paginator.ItemsPerPage);
            GroupList.DataSource = usersGroups;
            GroupListCurrentRow = 0;
            GroupList.DataBind();
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

                CheckBox cbJoin = (CheckBox)e.Item.FindControl("cbJoin");
                cbJoin.Checked = MyGroupIdList.Contains(item.usersGroup.id);
                cbJoin.Enabled = (UserIsLogged && !cbJoin.Checked);

                HiddenField hfId = (HiddenField)e.Item.FindControl("hfId");
                hfId.Value = item.usersGroup.id.ToString();
            }
        }

        protected void BtnJoinGroupsClick(object sender, EventArgs e)
        {
            if (!UserIsLogged)
            {
                lblLoginRequired.Visible = true;
                return;
            }

            List<long> usersGroupIds = new List<long>();
            List<RepeaterItem> addedItems = new List<RepeaterItem>();

            foreach (RepeaterItem item in GroupList.Items)
            {
                if (item.ItemType == ListItemType.Item
                    || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox cbJoin = (CheckBox)item.FindControl("cbJoin");
                    if (cbJoin.Checked && cbJoin.Enabled)
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

            if (usersGroupIds.Count > 0)
            {
                try
                {
                    // use service to add the user to the groups
                    UsersGroupService.AddUserToGroup(usersGroupIds, UserProfileId);

                    // show success feedback
                    lblUserAdded.Visible = true;

                    // update view
                    foreach (RepeaterItem item in addedItems)
                    {
                        // disable checkbox
                        ((CheckBox)item.FindControl("cbJoin")).Enabled = false;
                        // increment user count
                        Label lblUserCount = (Label)item.FindControl("lblUserCount");
                        lblUserCount.Text = (Int64.Parse(lblUserCount.Text) + 1L).ToString();
                    }

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

        private List<long> GroupDtoListToIdList(List<UsersGroupDto> usersGroupDtoList)
        {
            List<long> list = new List<long>();
            foreach (UsersGroupDto usersGroupDto in usersGroupDtoList)
            {
                list.Add(usersGroupDto.usersGroup.id);
            }
            return list;
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