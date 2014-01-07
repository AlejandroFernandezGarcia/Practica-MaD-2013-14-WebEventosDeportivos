﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
//TODO Colocar CSS
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class AddComment : System.Web.UI.Page
    {
        private readonly IEventService eventService =
           UnityResolver.Resolve<IEventService>();

        private long eventId;

        protected void Page_Load(object sender, EventArgs e)
        {
            String eventStrId = Request.QueryString["eventId"];
            eventId = Convert.ToInt64(eventStrId);
            lblEmptyComment.Visible = false;
            lblCommentSuccess.Visible = false;
            lblCommentMaxLength.Visible = false;
            lblTagMaxLenght.Visible = false;

            NewComment.ToolTip = (String)GetLocalResourceObject("lclCommentTip.Text");
            NewTags.ToolTip = (String)GetLocalResourceObject("lclTagInstructions.Text");

            try
            {
                Model.Event evento = eventService.FindById(Convert.ToInt64(eventStrId));
                lblEventNameExt.Text = evento.name;
                
            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
            }
            

        }

        protected void BtnSendComment(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String text = NewComment.Text;
                if (text.Length == 0)
                {
                    lblEmptyComment.Visible = true;
                }
                else if (text.Length >= 1000)
                {
                    lblCommentMaxLength.Visible = true;
                }else
                {
                    UserSession userSession = SessionManager.GetUserSession(Context);
                    if (userSession == null)
                    {
                        Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
                    }
                    else //comentario y usuario correctos
                    {
                        List<String> tags = new List<string>();
                        String tagsString = NewTags.Text;

                        if ((tagsString.Length != 0))
                        {
                            String[] vTags = tagsString.Split(',');
                            foreach (var t in vTags)
                            {
                                if (t.Length >= 50)
                                {
                                    lblTagMaxLenght.Visible = true;
                                    break;
                                }
                                tags.Add(t);
                            }
                        }
                        if (!lblTagMaxLenght.Visible)
                        {
                            eventService.AddComment(eventId, text, userSession.UserProfileId, tags);
                            lblCommentSuccess.Visible = true;

                            Thread.Sleep(1000);
                            Response.Redirect(Response.ApplyAppPathModifier(Request.UrlReferrer.ToString()));
                        }
                    }
                }
            }
        }

        protected void BtnCancelComment(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(Response.ApplyAppPathModifier(Request.UrlReferrer.ToString()));
            }
        }
    }
}