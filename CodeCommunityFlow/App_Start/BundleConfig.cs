using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CodeCommunityFlow.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundle)
        {
            //bundle.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/jquery-3.7.1.js",
            //    "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));


            bundle.Add(new StyleBundle("~/Styles/bootstrap").Include("~/Content/bootstrap.css"));
            bundle.Add(new StyleBundle("~/Styles/site").Include("~/Content/StyleSheet1.css"));

            BundleTable.EnableOptimizations = true;

        }
    }
}