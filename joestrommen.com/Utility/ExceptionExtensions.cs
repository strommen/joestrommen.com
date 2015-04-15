using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace joestrommen.com.Utility
{
	public static class ExceptionExtensions
	{
		public static void LogToElmah(this Exception ex, string newMessage = null)
		{
			if (ex is System.Threading.ThreadAbortException ||
				((ex is AggregateException) && ((AggregateException)ex).InnerExceptions.All(e => e is System.Threading.ThreadAbortException)))
			{
				// We get this whenever the app recycles, and it's just noise.
				return;
			}

			if (newMessage != null)
			{
				ex = new Exception(newMessage, ex);
			}

			Email.NotifyJoeAsync(ex.Message, ex.ToString()).Wait(TimeSpan.FromSeconds(10));

			if (HttpContext.Current != null)
			{
				ErrorSignal.FromCurrentContext().Raise(ex);
			}
			else
			{
				if (httpApplication == null) InitNoContext();
				ErrorSignal.Get(httpApplication).Raise(ex);
			}
		}

		private static HttpApplication httpApplication = null;
		private static ErrorFilterConsole errorFilter = new ErrorFilterConsole();

		public static ErrorMailModule ErrorEmail = new ErrorMailModule();
		public static ErrorLogModule ErrorLog = new ErrorLogModule();
		public static ErrorTweetModule ErrorTweet = new ErrorTweetModule();

		private static void InitNoContext()
		{
			httpApplication = new HttpApplication();
			errorFilter.Init(httpApplication);

			(ErrorEmail as IHttpModule).Init(httpApplication);
			errorFilter.HookFiltering(ErrorEmail);
			ErrorEmail.Mailing += ErrorEmail_Mailing;
			ErrorEmail.Mailed += ErrorEmail_Mailed;

			(ErrorLog as IHttpModule).Init(httpApplication);
			errorFilter.HookFiltering(ErrorLog);

			(ErrorTweet as IHttpModule).Init(httpApplication);
			errorFilter.HookFiltering(ErrorTweet);
		}

		private static void ErrorEmail_Mailing(object sender, ErrorMailEventArgs args)
		{

		}

		private static void ErrorEmail_Mailed(object sender, ErrorMailEventArgs args)
		{

		}

		private class ErrorFilterConsole : ErrorFilterModule
		{
			public void HookFiltering(IExceptionFiltering module)
			{
				module.Filtering += new ExceptionFilterEventHandler(base.OnErrorModuleFiltering);
			}
		}
	}
}