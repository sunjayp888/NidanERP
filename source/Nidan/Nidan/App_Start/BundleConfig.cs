using System.Web.Optimization;

namespace Nidan
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.ResetAll();

            bundles.Add(new ScriptBundle("~/Scripts/bower").Include(
                "~/bower_components/jquery/dist/jquery.min.js",
               // "~/bower_components/jquery/dist/jquery.ui.min.js",
                "~/bower_components/jquery-validation/dist/jquery.validate.min.js",
                "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js",
                "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
                "~/bower_components/moment/min/moment.min.js",
                "~/bower_components/angular/angular.min.js",
                "~/bower_components/angular-animate/angular-animate.min.js",
                "~/bower_components/angular-sanitize/angular-sanitize.min.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",
                "~/bower_components/angular-responsive-tables/release/angular-responsive-tables.min.js",
                "~/bower_components/angular-ui-select/dist/select.min.js",
                "~/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                "~/bower_components/bootbox/bootbox.js",
                "~/bower_components/ngBootbox/ngBootbox.js",
                "~/bower_components/cropper/dist/cropper.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/bower/gentelella").Include(
                "~/bower_components/gentelella/build/js/custom.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Application").Include(
                "~/Scripts/Angular/Moment.js",
                "~/Scripts/Angular/app.js",
                "~/Scripts/Angular/Prototypes/*.js",
                "~/Scripts/Angular/Controllers/*.js",
                "~/Scripts/moris/*.js",
                "~/Scripts/Angular/Services/*.js",
                "~/Scripts/Angular/Filters/*.js",
                "~/Scripts/Angular/Directives/*.js",
                "~/Scripts/jquery-validate-bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/getOrgChart").Include(
                "~/Scripts/getorgchart.js"
                ));

            bundles.Add(new StyleBundle("~/Content/bower").Include(
                 "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                 "~/bower_components/angular-responsive-tables/release/angular-responsive-tables.min.css",
                 "~/bower_components/angular-ui-select/dist/select.min.css",
                 "~/bower_components/font-awesome/css/font-awesome.min.css",
                 "~/bower_components/bootstrap-daterangepicker/daterangepicker.css",
                 "~/bower_components/less-space/dist/less-space.min.css",
                "~/bower_components/cropper/dist/cropper.min.css"
                 ));

            bundles.Add(new StyleBundle("~/Content/bower/gentelella").Include(
                "~/bower_components/gentelella/build/css/custom.min.css",
                "~/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/getOrgChart").Include(
                "~/Scripts/getorgchart.css"
                 ));

            bundles.Add(new StyleBundle("~/Content/Application").Include(
                "~/Content/css/Site.min.css"
                ));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
