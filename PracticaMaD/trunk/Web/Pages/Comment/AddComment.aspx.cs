using System;
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
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class AddComment : System.Web.UI.Page
    {
        private readonly IEventService eventService =
           UnityResolver.Resolve<IEventService>();

        private const int MAX_LENGTH_COMMENT = 1000;
        private const int MAX_LENGTH_TAG = 50;

        private long eventId;

        protected void Page_Load(object sender, EventArgs e)
        {
            String eventStrId = Request.QueryString["eventId"];
            eventId = Convert.ToInt64(eventStrId);

            if (!IsPostBack)
            {
                ViewState["retUrl"] = "~" + Request.UrlReferrer.ToString().Substring(21);
            }

            lblEmptyComment.Visible = false;
            lblCommentSuccess.Visible = false;
            lblCommentMaxLength.Visible = false;
            lblTagMaxLenght.Visible = false;
            NewComment.MaxLength = MAX_LENGTH_COMMENT;
            NewTags.MaxLength = MAX_LENGTH_TAG;

            NewComment.ToolTip = (String)GetLocalResourceObject("lclCommentTip.Text");
            NewTags.ToolTip = (String)GetLocalResourceObject("lclTagInstructions.Text");


            try
            {
                Model.Event evento = eventService.FindEventById(Convert.ToInt64(eventStrId));
                lclEventNameExt.Text = evento.name;
                
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
                else if (text.Length >= MAX_LENGTH_COMMENT)
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
                                if (t.Length >= MAX_LENGTH_TAG)
                                {
                                    lblTagMaxLenght.Visible = true;
                                    break;
                                }
                                tags.Add(t.ToLower());
                            }
                        }
                        if (!lblTagMaxLenght.Visible)
                        {
                            eventService.AddComment(eventId, text, userSession.UserProfileId, tags);
                            lblCommentSuccess.Visible = true;
                            
                            Response.Redirect(Response.ApplyAppPathModifier(ViewState["retUrl"].ToString()+"&commentAdd=ok"));
                        }
                    }
                }
            }
        }


        protected void BtnReturn(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier(ViewState["retUrl"].ToString()));
        }
    }
}