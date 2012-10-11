using System;
using System.Web;

namespace Platform.Client.Common.Context
{
	public interface ICookieContextProvider
	{
		string Get(string name);
		void Set(string name, string value);
	}

	public class CookieContextProvider : ICookieContextProvider
	{
		public string Get(string name)
		{
			var val = HttpContext.Current.Request.Cookies.Get(name);
			return val == null ? null : val.Value;
		}

		public void Set(string name, string value)
		{
			var cookie = new HttpCookie(name, value) {Expires = DateTime.Now.AddHours(1)};

			HttpContext.Current.Response.Cookies.Set(cookie);
		}
	}
}
