using System.Collections.Generic;
using Platform.Client.Common.Context;

namespace SocialInsight.Domain.AppActions
{
	public interface IAppActionsApi
	{
		void Register(string caption, int canvasHeight, int canvasWidth);
	}

	public class AppActionsApi : ApiBase, IAppActionsApi
	{
		protected override string BasePath { get { return "app-actions/product"; } }

		public AppActionsApi(IContextProvider contextProvider) : base(contextProvider)
		{
		}

		public void Register(string text, int canvasHeight, int canvasWidth)
		{
			Call<string>("register", 
				new KeyValuePair<string, object>("text", text),
				new KeyValuePair<string, object>("canvasHeight", canvasHeight),
				new KeyValuePair<string, object>("canvasWidth", canvasWidth));
		}
	}
}
