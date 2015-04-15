using System.Web;
using System.Web.Optimization;

namespace joestrommen.com
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new Bundle("~/bundles/ie8_compatibility.js").Include(
						"~/Scripts/3rd-party/html5shiv.js",
						"~/Scripts/getComputedStyle.js",
						"~/Scripts/3rd-party/respond.js"));

			bundles.Add(new Bundle("~/bundles/twoStage.js").Include(
						"~/Scripts/twoStage.js"));

			bundles.Add(new Bundle("~/bundles/body_scripts.js").Include(
						"~/Scripts/3rd-party/instantclick.js",
						"~/Scripts/domUtil.js",
						"~/Scripts/common.js"));

			bundles.Add(new StyleBundle("~/bundles/site.css").Include(
					  "~/Content/site.css"));
		}
	}
}
