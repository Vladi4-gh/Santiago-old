using System.Web.Optimization;

namespace Santiago.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterCommonBundles(bundles);
            RegisterPageSpecificBundles(bundles);

            // Раскомментировать, если необходимо включить бандлы в режиме отладки.
            // BundleTable.EnableOptimizations = true;
        }

        private static void RegisterCommonBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/common/css")
                .Include("~/Static/common/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Static/common/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Static/common/css/styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/common/js")
                .Include("~/Static/common/js/jquery/jquery-{version}.js")
                .Include("~/Static/common/js/underscore/underscore*")
                .Include("~/Static/common/bootstrap/js/bootstrap*"));
        }

        private static void RegisterPageSpecificBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/admin-page-main-menu/css")
                .Include("~/Static/widgets/paginator/paginator.css")
                .Include("~/Static/widgets/popup/popup.css")
                .Include("~/Static/widgets/data-table/data-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-page-main-menu/js")
                .Include("~/Static/common/js/jquery-validate/jquery.validate*")
                .Include("~/Static/common/js/jquery-validate-unobtrusive/jquery.validate.unobtrusive*")
                .Include("~/Static/common/js/unobtrusive-validation-extensions.js")
                .Include("~/Static/common/js/doT/doT*")
                .Include("~/Static/common/js/dateFormat/dateFormat*")
                .Include("~/Static/widgets/paginator/paginator.js")
                .Include("~/Static/widgets/popup/popup.js")
                .Include("~/Static/widgets/data-table/data-table.js")
                .Include("~/Static/pages-js/admin/admin-page-main-menu.js"));


            bundles.Add(new StyleBundle("~/bundles/admin-page-pages/css")
                .Include("~/Static/widgets/paginator/paginator.css")
                .Include("~/Static/widgets/popup/popup.css")
                .Include("~/Static/widgets/data-table/data-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-page-pages/js")
                .Include("~/Static/common/js/jquery-validate/jquery.validate*")
                .Include("~/Static/common/js/jquery-validate-unobtrusive/jquery.validate.unobtrusive*")
                .Include("~/Static/common/js/unobtrusive-validation-extensions.js")
                .Include("~/Static/common/js/doT/doT*")
                .Include("~/Static/common/js/dateFormat/dateFormat*")
                .Include("~/Static/widgets/paginator/paginator.js")
                .Include("~/Static/widgets/popup/popup.js")
                .Include("~/Static/widgets/data-table/data-table.js")
                .Include("~/Static/pages-js/admin/admin-page-pages.js"));


            bundles.Add(new StyleBundle("~/bundles/admin-page-photograph-categories/css")
                .Include("~/Static/widgets/paginator/paginator.css")
                .Include("~/Static/widgets/popup/popup.css")
                .Include("~/Static/widgets/data-table/data-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-page-photograph-categories/js")
                .Include("~/Static/common/js/jquery-validate/jquery.validate*")
                .Include("~/Static/common/js/jquery-validate-unobtrusive/jquery.validate.unobtrusive*")
                .Include("~/Static/common/js/unobtrusive-validation-extensions.js")
                .Include("~/Static/common/js/doT/doT*")
                .Include("~/Static/common/js/dateFormat/dateFormat*")
                .Include("~/Static/widgets/paginator/paginator.js")
                .Include("~/Static/widgets/popup/popup.js")
                .Include("~/Static/widgets/data-table/data-table.js")
                .Include("~/Static/pages-js/admin/admin-page-photograph-categories.js"));


            bundles.Add(new StyleBundle("~/bundles/admin-page-photographs/css")
                .Include("~/Static/widgets/file-upload/file-upload.css")
                .Include("~/Static/widgets/paginator/paginator.css")
                .Include("~/Static/widgets/popup/popup.css")
                .Include("~/Static/widgets/data-table/data-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-page-photographs/js")
                .Include("~/Static/common/js/jquery-validate/jquery.validate*")
                .Include("~/Static/common/js/jquery-validate-unobtrusive/jquery.validate.unobtrusive*")
                .Include("~/Static/common/js/unobtrusive-validation-extensions.js")
                .Include("~/Static/common/js/doT/doT*")
                .Include("~/Static/common/js/dateFormat/dateFormat*")
                .Include("~/Static/widgets/file-upload/file-upload.js")
                .Include("~/Static/widgets/paginator/paginator.js")
                .Include("~/Static/widgets/popup/popup.js")
                .Include("~/Static/widgets/data-table/data-table.js")
                .Include("~/Static/pages-js/admin/admin-page-photographs.js"));


            bundles.Add(new StyleBundle("~/bundles/admin-page-site-settings/css")
                .Include("~/Static/widgets/paginator/paginator.css")
                .Include("~/Static/widgets/popup/popup.css")
                .Include("~/Static/widgets/data-table/data-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-page-site-settings/js")
                .Include("~/Static/common/js/jquery-validate/jquery.validate*")
                .Include("~/Static/common/js/jquery-validate-unobtrusive/jquery.validate.unobtrusive*")
                .Include("~/Static/common/js/unobtrusive-validation-extensions.js")
                .Include("~/Static/common/js/doT/doT*")
                .Include("~/Static/common/js/dateFormat/dateFormat*")
                .Include("~/Static/widgets/paginator/paginator.js")
                .Include("~/Static/widgets/popup/popup.js")
                .Include("~/Static/widgets/data-table/data-table.js")
                .Include("~/Static/pages-js/admin/admin-page-site-settings.js"));


            bundles.Add(new StyleBundle("~/bundles/admin-page-testimonials/css")
                .Include("~/Static/widgets/file-upload/file-upload.css")
                .Include("~/Static/widgets/paginator/paginator.css")
                .Include("~/Static/widgets/popup/popup.css")
                .Include("~/Static/widgets/data-table/data-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-page-testimonials/js")
                .Include("~/Static/common/js/jquery-validate/jquery.validate*")
                .Include("~/Static/common/js/jquery-validate-unobtrusive/jquery.validate.unobtrusive*")
                .Include("~/Static/common/js/unobtrusive-validation-extensions.js")
                .Include("~/Static/common/js/doT/doT*")
                .Include("~/Static/common/js/dateFormat/dateFormat*")
                .Include("~/Static/widgets/file-upload/file-upload.js")
                .Include("~/Static/widgets/paginator/paginator.js")
                .Include("~/Static/widgets/popup/popup.js")
                .Include("~/Static/widgets/data-table/data-table.js")
                .Include("~/Static/pages-js/admin/admin-page-testimonials.js"));


            bundles.Add(new StyleBundle("~/bundles/admin-page-users/css")
                .Include("~/Static/widgets/paginator/paginator.css")
                .Include("~/Static/widgets/popup/popup.css")
                .Include("~/Static/widgets/data-table/data-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-page-users/js")
                .Include("~/Static/common/js/jquery-validate/jquery.validate*")
                .Include("~/Static/common/js/jquery-validate-unobtrusive/jquery.validate.unobtrusive*")
                .Include("~/Static/common/js/unobtrusive-validation-extensions.js")
                .Include("~/Static/common/js/doT/doT*")
                .Include("~/Static/common/js/dateFormat/dateFormat*")
                .Include("~/Static/common/js/editor-extensions.js")
                .Include("~/Static/widgets/paginator/paginator.js")
                .Include("~/Static/widgets/popup/popup.js")
                .Include("~/Static/widgets/data-table/data-table.js")
                .Include("~/Static/pages-js/admin/admin-page-users.js"));


            bundles.Add(new ScriptBundle("~/bundles/page-log-in/js")
                .Include("~/Static/common/js/jquery-validate/jquery.validate*")
                .Include("~/Static/common/js/jquery-validate-unobtrusive/jquery.validate.unobtrusive*")
                .Include("~/Static/common/js/unobtrusive-validation-extensions.js")
                .Include("~/Static/common/js/editor-extensions.js")
                .Include("~/Static/pages-js/page-log-in.js"));


            bundles.Add(new StyleBundle("~/bundles/page-photograph-gallery/css")
                .Include("~/Static/widgets/photograph-gallery/photograph-gallery-slider.css")
                .Include("~/Static/widgets/photograph-gallery/photograph-gallery.css"));

            bundles.Add(new ScriptBundle("~/bundles/page-photograph-gallery/js")
                .Include("~/Static/widgets/photograph-gallery/photograph-gallery-slider.js")
                .Include("~/Static/widgets/photograph-gallery/photograph-gallery.js")
                .Include("~/Static/pages-js/page-photograph-gallery.js"));
        }
    }
}