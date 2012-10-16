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

		public AppActionsApi(IContextProvider contextProvider)
			: base(contextProvider)
		{
		}

		public void Register(string text, int canvasHeight, int canvasWidth)
		{
			Call<string>("register", new
			{
				Text = text,
				CanvasHeight = canvasHeight,
				CanvasWidth = canvasWidth
			});
		}
	}
}
