using System;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Errors
{

    public partial class InternalError : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            LogManager.RecordMessage(this.GetType().Name + " instantiated.", MessageType.Error);
        }
    }
}
