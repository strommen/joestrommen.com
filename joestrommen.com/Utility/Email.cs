using Mandrill;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace joestrommen.com.Utility
{
	public static class Email
	{
		public static async Task Send(MailAddress from, MailAddress to, string subject, string body, bool isBodyHtml = true, MailAddress replyTo = null, string bccAddress = null)
		{
			var mandrill = new MandrillApi(ConfigurationManager.AppSettings["MandrillApiKey"]);

			var email = new EmailMessage()
			{
				from_email = from.Address,
				from_name = from.DisplayName,
				to = new[] { new EmailAddress(to.Address, to.DisplayName) },
				subject = subject,
				bcc_address = bccAddress,
			};

			if (isBodyHtml)
			{
				email.html = body;
			}
			else
			{
				email.text = body;
			}

			if (replyTo != null)
			{
				email.AddHeader("Reply-To", replyTo.ToString());
			}

			var results = await mandrill.SendMessageAsync(email);
			var result = results.Single();

			switch (result.Status)
			{
				case EmailResultStatus.Sent:
				case EmailResultStatus.Queued:
				case EmailResultStatus.Scheduled:
					// All is good.
					break;
				case EmailResultStatus.Rejected:
					throw new Exception("Email rejected: " + result.RejectReason);
				case EmailResultStatus.Invalid:
				default:
					throw new Exception("Email failed with status: " + result.Status);
			}
		}

		public static async Task NotifyJoeAsync(string message, string detail = "")
		{
			try
			{
				await Email.Send(
					from: new MailAddress("joe@joestrommen.com", "joestrommen.com Auto-Notifier"),
					to: new MailAddress("joe@joestrommen.com"),
					subject: message,
					body: detail,
					isBodyHtml: false);
			}
			catch (Exception ex)
			{
				ex.LogToElmah("Unable to notify Joe");
			}
		}
	}
}