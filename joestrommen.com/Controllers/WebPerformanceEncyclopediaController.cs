using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace joestrommen.com.Controllers
{
    public class WebPerformanceEncyclopediaController : Controller
    {
        public ActionResult Index(string id = null)
        {
			if (string.IsNullOrEmpty(id))
			{
				return View();
			}
			else
			{
				return View(viewName: id);
			}
        }
	}
}