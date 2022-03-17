using System.Web;
using System.Web.Optimization;

namespace OfficeFiddleMVC
{
  public class BundleConfig
  {
    // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
    public static void RegisterBundles(BundleCollection bundles)
    {

      bundles.Add(new ScriptBundle("~/bundles/js").Include(
               "~/Scripts/jquery-{version}.js",
               "~/Scripts/modernizr-*",
               "~/Scripts/bootstrap.js",
               "~/Scripts/respond.js"
              ));
      bundles.Add(new ScriptBundle("~/bundles/app").Include(
     "~/Scripts/app/app.js",
     "~/Scripts/app/home/home.js",
     "~/Scripts/codemirror-2.37/lib/codemirror.js",
     "~/Scripts/codemirror-2.37/mode/javascript/javascript.js",
     "~/Scripts/codemirror-2.37/mode/xml/xml.js",
     "~/Scripts/codemirror-2.37/mode/css/css.js"
    ));

      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/app.css",        
               "~/Content/font-awesome.css",
                "~/Scripts/codemirror-2.37/lib/codemirror.css"));

    }
    
  }
}
