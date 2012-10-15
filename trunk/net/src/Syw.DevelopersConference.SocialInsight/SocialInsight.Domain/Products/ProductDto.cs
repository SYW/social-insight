namespace SocialInsight.Domain.Products
{
	public class ProductDto
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string ProductUrl { get; set; }
		public decimal Price { get; set; }
	}
}
