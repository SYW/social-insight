using System;

namespace SocialInsight.Domain.Catalogs
{
	public class CatalogDto
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public CatalogItemDto[] Items { get; set; }
	}

	public class CatalogItemDto
	{
		public long Id { get; set; }
		public DateTime AddedOn { get; set; }
	}
}
