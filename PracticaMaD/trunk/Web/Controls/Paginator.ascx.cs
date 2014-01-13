using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Controls
{
    public partial class Paginator : System.Web.UI.UserControl
    {
        public String NavigateUrl { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }

        private String _pagingUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LastPage < 2)
            {
                Container.Visible = false;
                return;
            }

            // make paging url
            _pagingUrl = NavigateUrl;
            _pagingUrl += (NavigateUrl.Contains("?")) ? "&" : "?";
            _pagingUrl += "page=";

            // asign url and text to links
            SetUrl(FirstLink, 1);
            SetUrl(LastLink, LastPage);
            SetRelativeUrl(PrevLink, -1);
            SetRelativeUrl(NextLink, 1);
            SetRelativeUrl(Middle1Link, -2);
            SetRelativeUrl(Middle2Link, -1);
            SetRelativeUrl(Middle3Link,  0);
            SetRelativeUrl(Middle4Link,  1);
            SetRelativeUrl(Middle5Link,  2);

            // hide non-needed links (from the begining)
            if (CurrentPage < 5)
            {
                SpaceFirstTd.Visible = false;
                if (CurrentPage < 4)
                {
                    FirstTd.Visible = false;
                    if (CurrentPage < 3)
                    {
                        Middle1Td.Visible = false;
                        if (CurrentPage < 2)
                        {
                            Middle2Td.Visible = false;
                            PrevTd.Visible = false;
                        }
                    }
                }
            }

            // hide non-needed links (from the end)
            var toLast = LastPage - CurrentPage;
            if (toLast < 4)
            {
                SpaceLastTd.Visible = false;
                if (toLast < 3)
                {
                    LastTd.Visible = false;
                    if (toLast < 2)
                    {
                        Middle5Td.Visible = false;
                        if (toLast < 1)
                        {
                            Middle4Td.Visible = false;
                            NextTd.Visible = false;
                        }
                    }
                }
            }
        }

        private void SetRelativeUrl(HyperLink link, int increment)
        {
            SetUrl(link, CurrentPage + increment);
        }

        private void SetUrl(HyperLink link, int page)
        {
            link.NavigateUrl = _pagingUrl + page.ToString();
            if (String.IsNullOrEmpty(link.Text))
            {
                link.Text = page.ToString();
            }
        }
    }
}