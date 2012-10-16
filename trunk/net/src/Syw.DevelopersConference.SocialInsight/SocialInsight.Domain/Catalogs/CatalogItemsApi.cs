using System.Collections.Generic;
using Platform.Client.Common.Context;

namespace SocialInsight.Domain.Catalogs
{
	public interface ICatalogItemsApi
	{
		void AddProductToCatalogs(IList<long> catalogIds, long productId);
	}

	public class CatalogItemsApi : ApiBase, ICatalogItemsApi
	{
		protected override string BasePath { get { return "catalogs/items"; } }

		public CatalogItemsApi(IContextProvider contextProvider)
			: base(contextProvider)
		{
		}

		public void AddProductToCatalogs(IList<long> catalogIds, long productId)
		{
			Call<string>("add", new
			{
				CatalogIds = catalogIds,
				ItemId = productId
			});
		}
	}
}
