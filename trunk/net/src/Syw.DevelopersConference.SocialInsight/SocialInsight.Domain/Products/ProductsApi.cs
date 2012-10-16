using System.Collections.Generic;
using Platform.Client.Common.Context;

namespace SocialInsight.Domain.Products
{
	public interface IProductsApi
	{
		IList<ProductDto> Get(IList<long> ids);
	}

	public class ProductsApi : ApiBase, IProductsApi
	{
		public ProductsApi(IContextProvider contextProvider)
			: base(contextProvider)
		{
		}

		protected override string BasePath { get { return "products"; } }

		public IList<ProductDto> Get(IList<long> ids)
		{
			return Call<ProductDto[]>("get", new { Ids = ids });
		}
	}
}
