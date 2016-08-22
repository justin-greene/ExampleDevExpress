using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Linq;


namespace DXWebApplication5.Helpers
{
    public static class PresentationExtensions
    {
        public static IHtmlString Image(this HtmlHelper Html, string url, string alt = "", string class_ = "", object attributes = null)
        {
            url = Html.FixProtocolForHref(url);

            string html = "<img src=\"{0}\" alt=\"{1}\"{2}/>";
            return new HtmlString(string.Format(html, Html.Content(url), alt, null));
        }

        public static string Content(this HtmlHelper Html, string url)
        {
            UrlHelper Url = new UrlHelper(new RequestContext(Html.ViewContext.HttpContext, Html.ViewContext.RouteData));
            return Url.Content(url);
        }

        internal static string FixProtocolForHref(this HtmlHelper Html, string url)
        {
            Func<string, string> regexEncode = (d) => "^http://" + d.Replace(".", "\\.").Replace("*", "[^.]+");

            return url;
        }
    }
}