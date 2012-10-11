using System.Collections.Generic;

namespace SocialInsight.Domain.Products
{
	public interface IProductsApi
	{
		IList<ProductDto> Get(IList<long> ids);
	}

	public class ProductsApi : ApiBase, IProductsApi
	{
		protected override string BasePath { get { return "products"; } }

		public IList<ProductDto> Get(IList<long> ids)
		{
			return Proxy.Get<ProductDto[]>(GetEndpointPath("get"), new KeyValuePair<string, object>("ids", ids));
		}
	}
}
