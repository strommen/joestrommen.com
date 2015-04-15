using joestrommen.com.Utility;
using MailChimp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace joestrommen.com.Controllers
{
	public class EmailController : AsyncController
    {
		public static bool HasEmailCookie(HttpRequest request = null)
		{
			return System.Web.HttpContext.Current.Request.Cookies["euid"] != null;
		}

		public static void SetEmailCookie(string euid)
		{
			System.Web.HttpContext.Current.Response.SetCookie(new HttpCookie("euid", euid)
			{
				Domain = "joestrommen.com",
				HttpOnly = false,
				Shareable = false,
				Expires = DateTime.Now + TimeSpan.FromDays(3650),
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ContentResult> Signup(string emailAddress)
		{
			if (!ModelState.IsValid)
			{
				return Content("Please enter a valid email address");
			}

			bool success = false;
			try
			{
				var chimp = new MailChimpManager(ConfigurationManager.AppSettings["MailChimpApiKey"]);
				var result = chimp.Subscribe(
					ConfigurationManager.AppSettings["MailChimpListId"],
					new MailChimp.Helper.EmailParameter()
					{
						Email = emailAddress,
					});
					//new MyMergeVar()
					//{
					//	Name = capture.Name,
					//});
				SetEmailCookie(result.EUId);

				success = true;
			}
			catch (Exception ex)
			{
				ex.LogToElmah(string.Format("Unable to capture email address: {0}", ex.Message));
			}

			await Email.NotifyJoeAsync("New email signup", emailAddress);

			return Content("Thanks for signing up! You'll receive your first email shortly.");
		}

		[System.Runtime.Serialization.DataContract]
		public class MyMergeVar : MailChimp.Lists.MergeVar
		{
			[System.Runtime.Serialization.DataMember(Name = "NAME")]
			public string Name { get; set; }
		}
	}
}