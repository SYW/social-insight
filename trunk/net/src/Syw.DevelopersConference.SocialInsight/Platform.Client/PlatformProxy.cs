using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Platform.Client.Common;
using Platform.Client.Common.WebClient;
using Platform.Client.Configuration;

namespace Platform.Client
{
	public class PlatformProxy
	{
		private readonly IWebClientBuilder _webClientBuilder;
		private readonly IPlatformSettings _platformSettings;
		private readonly IPlatformTokenProvider _platformTokenProvider;
		private readonly IPlatformHashProvider _platformHashProvider;
		private readonly IParametersTranslator _parametersTranslator;

		public PlatformProxy(IPlatformSettings platformSettings, 
			IApplicationSettings applicationSettings,
			IPlatformTokenProvider platformTokenProvider)
		{
			_platformSettings = platformSettings;			
			_platformTokenProvider = platformTokenProvider;
			
			_parametersTranslator = new ParametersTranslator();
			_webClientBuilder = new WebClientBuilder();
			_platformHashProvider = new PlatformHashProvider(applicationSettings, platformTokenProvider);
		}

		public TR Get<TR>(string servicePath, params KeyValuePair<string, object>[] parameters)
		{
			return Get<TR>(GetServiceUrl(servicePath), parameters, AddContextParameters);
		}

		public TR GetWithoutContext<TR>(string servicePath, params KeyValuePair<string, object>[] parameters)
		{
			return Get<TR>(GetServiceUrl(servicePath), parameters, null);
		}

		public TR SecuredGetWithoutContext<TR>(string servicePath, params KeyValuePair<string, object>[] parameters)
		{
			var serviceUrl = GetSecureServiceUrl(servicePath);

			return Get<TR>(serviceUrl, parameters, null);
		}

		private TR Get<TR>(Uri serviceUrl, ICollection<KeyValuePair<string, object>> parameters, Action<NameValueCollection> applyExtraParameters)
		{
			var webClient = _webClientBuilder.Create();

			NameValueCollection serviceParameters;

			if (parameters == null)
			{
				serviceParameters = new NameValueCollection(2);
			}
			else
			{
				serviceParameters = new NameValueCollection(parameters.Count + 2);

				foreach (var parameter in parameters)
				{
					var value = _parametersTranslator.ToJson(parameter);

					serviceParameters.Add(parameter.Key, value);
				}

			}

			if (applyExtraParameters != null)
				applyExtraParameters(serviceParameters);

			try
			{
				return webClient.GetJson<TR>(serviceUrl, serviceParameters);
			}
			catch (WebException ex)
			{
				throw GeneratePlatformRequestException(ex);
			}
		}

		private Exception GeneratePlatformRequestException(WebException ex)
		{
			try
			{
				var readError = ReadError(ex);
				var errorDto = JsonConvert.DeserializeObject<RequestExceptionDto>(readError).Error;

				return new RequestException(errorDto.StatusCode, errorDto.Message, errorDto.RequestId, ex);
			}
			catch (Exception)
			{
				return ex;
			}
		}

		private string ReadError(WebException ex)
		{
			using (var reader = new StreamReader(ex.Response.GetResponseStream(), Encoding.UTF8))
			{
				return reader.ReadToEnd();
			}	
		}

		private void AddContextParameters(NameValueCollection serviceParameters)
		{
			serviceParameters.Add("token", _platformTokenProvider.Get());
			serviceParameters.Add("hash", _platformHashProvider.GetHash());
		}

		private Uri GetServiceUrl(string servicePath)
		{
			return new Uri(_platformSettings.ApiUrl, servicePath);
		}

		private Uri GetSecureServiceUrl(string servicePath)
		{
			return new Uri(_platformSettings.SecureApiUrl, servicePath);
		}
	}
}
