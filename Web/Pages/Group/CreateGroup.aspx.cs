﻿using System;
using Es.Udc.DotNet.PracticaMaD.Model.UsersGroupService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Group
{
    public partial class CreateGroup : SpecificCulturePage
    {

        protected readonly IUsersGroupService UsersGroupService =
            UnityResolver.Resolve<IUsersGroupService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // hide labels
            lblGroupCreated.Visible = false;
            lblGroupNameError.Visible = false;
            lblGroupDescriptionError.Visible = false;
        }

        protected void BtnCreateGroupClick(object sender, EventArgs e)
        {
            lblGroupCreated.Visible = false;

            if (Page.IsValid)
            {
                try
                {
                    // try to create the new group
                    UsersGroupService.Create(txtGroupName.Text, txtGroupDescription.Text,
                        SessionManager.GetUserSession(Context).UserProfileId);

                    // show success feedback
                    lblGroupCreated.Visible = true;

                    // clear form
                    txtGroupName.Text = "";
                    lblGroupNameError.Visible = false;
                    txtGroupDescription.Text = "";
                    lblGroupDescriptionError.Visible = false;
                }
                catch (NullReferenceException)
                {
                    // this happens when SessionManager.GetUserSession returns null,
                    // so we force logout and redirect to the authentication page
                    SessionManager.Logout(Context);
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/Authentication.aspx"
                        + "?ReturnUrl=%2fPages%2fGroup%2fCreateGroup.aspx"));
                }
                catch (DuplicateInstanceException)
                {
                    lblGroupNameError.Visible = true;
                }
            }
        }
    }
}