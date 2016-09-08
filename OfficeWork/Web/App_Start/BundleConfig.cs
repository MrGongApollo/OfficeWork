﻿using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                      "~/Content/bootstrap/js/bootstrap.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Scripts/base.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-daterangepicker/js").Include(
                        "~/Content/bootstrap-daterangepicker/js/moment.js",
                        "~/Content/bootstrap-daterangepicker/js/daterangepicker.js"

                        ));


            bundles.Add(new StyleBundle("~/bundles/bootstrap-daterangepicker/css").Include(
                        "~/Content/bootstrap-daterangepicker/css/daterangepicker-bs3.css"

                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table/js").Include(
                        "~/Content/bootstrap-table/js/bootstrap-table.min.js",
                        "~/Content/bootstrap-table/js/locale/bootstrap-table-zh-CN.min.js",
                        "~/Content/bootstrap-table/js/extensions/filter-control/bootstrap-table-filter-control.js"
                        ));


            bundles.Add(new StyleBundle("~/bundles/bootstrap-table/css").Include(
                        "~/Content/bootstrap-table/css/bootstrap-table.min.css"));

            bundles.Add(new StyleBundle("~/bundles/commoncss").Include(
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/font-awesome/css/font-awesome-ie7.min.css",
                      "~/Content/common.min.css"));
        }
    }
}