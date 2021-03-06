﻿using System.Web.Optimization;

namespace AudioNetwork.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
      
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                        "~/Scripts/fileinput.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jasny-bootstrap.css",
                      "~/Content/bootstrap-fileinput/css/fileinput.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/site.css",
                      "~/Content/SimpleChat.css",
                      "~/Content/textAngular.css"));

            bundles.Add(new ScriptBundle("~/bundles/AngularControllers")
                .IncludeDirectory("~/Scripts/Controllers", "*.js")
                .IncludeDirectory("~/Scripts/Controllers/KnowledgeSession", "*.js"));


            bundles.Add(new ScriptBundle("~/bundles/AngularServices")
                .IncludeDirectory("~/Scripts/Factories", "*.js"));


            bundles.Add(new ScriptBundle("~/bundles/Recognize")
                .IncludeDirectory("~/Scripts/Recognize", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/Libs")
                .IncludeDirectory("~/Scripts/Libs", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/Angular")
                .IncludeDirectory("~/Scripts/Libs/Angular", "*.js")
                .IncludeDirectory("~/Scripts/Libs/angular-ui", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/textAngular")
                .Include("~/Scripts/Libs/textAngular/textAngular-rangy.min.js",
                    "~/Scripts/Libs/textAngular/textAngular-sanitize.js",
                    "~/Scripts/Libs/textAngular/textAngular.js",
                    "~/Scripts/Libs/textAngular/textAngularSetup.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Controller/*.js"));
        }
    }
}
