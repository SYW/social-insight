using System.Collections.Generic;
using Platform.Client;
using Platform.Client.Common.Context;
using SocialInsight.Domain.Configuration;

namespace SocialInsight.Domain
{
	public abstract class ApiBase
	{
		protected PlatformProxy Proxy { get; private set; }

		protected abstract string BasePath { get; }

		protected ApiBase()
		{
			Proxy = new PlatformProxy(new PlatformSettings(),
									   new ApplicationSettings(),
									   new PlatformTokenProvider(new HttpContextProvider()));
		}

		protected string GetEndpointPath(string endpoint)
		{
			return string.Format("/{0}/{1}", BasePath, endpoint);
		}

		protected T Call<T>(string endpoint, params KeyValuePair<string, object>[] parameters)
		{
			return Proxy.Get<T>(GetEndpointPath(endpoint), parameters);
		}
	}
}
