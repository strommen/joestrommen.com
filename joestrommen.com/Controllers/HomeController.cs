using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace joestrommen.com.Controllers
{
	public class HomeController : Controller
	{
		//private Dictionary<string, string> RedirectMap = new Dictionary<string, string>()
		//{
		//	{"real-world-studies-of-website-performance", "real-world-impact-of-website-performance"},
		//};

		public ActionResult Index(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return View();
			}

			//string redirect;
			//if (RedirectMap.TryGetValue(id, out redirect))
			//{
			//	return RedirectToActionPermanent("Index", new { @id = redirect });
			//}

			return View(viewName: id);
		}
	}
}