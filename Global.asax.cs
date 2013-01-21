using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Web.Optimization;

namespace abrie.netWeb
{
    public class Global : System.Web.HttpApplication
    {
        public class PlainJsBundler : IBundleTransform
        {
            public virtual void Process(BundleResponse bundle)
            {
                bundle.ContentType = "text/javascript";
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            /*BundleTable.Bundles.EnableDefaultBundles();
#if DEBUG
            var b = new Bundle("~/jss", typeof(PlainJsBundler));
#else
            var b = new Bundle("~/jss", typeof(JsMinify));
#endif

            b.AddFile("~/assets/js/modernizr-latest.js");
            b.AddFile("~/assets/js/jquery-1.6.4.min.js");
            b.AddFile("~/assets/js/master.js");
            BundleTable.Bundles.Add(b);

            new Bundle("~/codejss", typeof(JsMinify));
            b.AddFile("~/assets/js/shCore.js");
            b.AddFile("~/assets/js/shBrushJScript.js");
            b.AddFile("~/assets/js/shBrushXml.js");
            b.AddFile("~/assets/js/jquery.tmpl.min.js");
            b.AddFile("~/assets/js/code.js");
            BundleTable.Bundles.Add(b);           

            var s = new Bundle("~/css", typeof(CssMinify));
            s.AddFile("~/assets/css/reset-min.css");
            s.AddFile("~/assets/css/styles.css");
            BundleTable.Bundles.Add(s);

            s = new Bundle("~/codecss", typeof(CssMinify));
            s.AddFile("~/assets/css/shCore.css");
            s.AddFile("~/assets/css/shThemeDefault.css");
            BundleTable.Bundles.Add(s);*/

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                HttpContext.Current.Response.End();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}