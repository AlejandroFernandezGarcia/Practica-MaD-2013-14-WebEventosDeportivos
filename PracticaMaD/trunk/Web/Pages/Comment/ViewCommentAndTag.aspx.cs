using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class ViewCommentAndTag : System.Web.UI.Page
    {

        private readonly IEventService eventService =
           UnityResolver.Resolve<IEventService>();

        private readonly ITagService tagService =
           UnityResolver.Resolve<ITagService>();

        private long commentId;
        private long eventId;

        protected void Page_Load(object sender, EventArgs e)
        {
            String eventStrId = Request.QueryString["commentId"];
            commentId = Convert.ToInt64(eventStrId);

            if (!IsPostBack)
            {
                ViewState["retUrl"] = "~" + Request.UrlReferrer.ToString().Substring(21);
            }

            try
            {
                Model.Comment comment = eventService.FindCommentById(Convert.ToInt64(eventStrId));
                Model.Event evento = eventService.FindEventById(comment.eventId);
                lclEventNameExt.Text = evento.name;
                eventId = evento.id;

                EditComment.Text = String.Copy(comment.text);
                List<Model.Tag> tags = tagService.FindTagsOfComment(commentId).ToList();

                String tagsToText = "";
                foreach (Model.Tag t in tags)
                {
                    tagsToText += t.tagName;
                    if(!t.Equals(tags.Last()))
                    {
                        tagsToText += ",";
                    }
                }

                EditTags.Text = tagsToText;

                EditComment.ToolTip = (String)GetLocalResourceObject("lclCommentTip.Text");
                EditTags.ToolTip = (String)GetLocalResourceObject("lclTagInstructions.Text");


                UserSession userSession = SessionManager.GetUserSession(Context);

                if (comment.userProfileId != userSession.UserProfileId)
                {
                    EditComment.Enabled = false;
                    EditTags.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                }
                else
                {
                    EditComment.Enabled = true;
                    EditTags.Enabled = true;
                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;
                }

            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
            }
        }

        protected void BtnEdit(object sender, EventArgs e)
        {
            lblEmptyComment.Visible = false;
            lblCommentMaxLength.Visible = false;
            lblCommentSuccess.Visible = false;
            lblTagMaxLenght.Visible = false;

            if (Page.IsValid)
            {
                
                String text = EditComment.Text;
                if (text.Length == 0)
                {
                    lblEmptyComment.Visible = true;
                }
                else if (text.Length >= 1000)
                {
                    lblCommentMaxLength.Visible = true;
                }
                else
                {
                    UserSession userSession = SessionManager.GetUserSession(Context);
                    if (userSession == null)
                    {
                        Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
                    }
                    else //comentario y usuario correctos
                    {
                        List<String> tags = new List<string>();
                        String tagsString = EditTags.Text;

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
                                tags.Add(t.ToLower());
                            }
                        }
                        if (!lblTagMaxLenght.Visible)
                        {
                            eventService.UpdateComment(commentId, text, tags);
                            lblCommentSuccess.Visible = true;

                            Response.Redirect(Response.ApplyAppPathModifier(ViewState["retUrl"].ToString()));
                        }
                    }
                }
            }
        }

        protected void BtnDelete(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                eventService.RemoveComment(eventId,commentId);
                //TODO feedback al volver en todos
                Response.Redirect(Response.ApplyAppPathModifier(ViewState["retUrl"].ToString()));
            }
        }

        protected void BtnReturn(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect(Response.ApplyAppPathModifier(ViewState["retUrl"].ToString()));
            }
        }
    }
}