using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Fakir.Web
{
    public static class AdminBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            //bundles.Add(
            //    new StyleBundle("~/Bundles/Admin/css")
            //        .IncludeDirectory("~/Common/Styles", "*.css", true)
            //    );

            bundles.Add(
                new ScriptBundle("~/Bundles/Admin/js")
                    .IncludeDirectory("~/libs/angular", "*.js", true)
                );

            BundleTable.EnableOptimizations = true;
        }
    }
}