namespace SocialInsight.Domain.Products
{
	public class ProductInsightDto
	{
		public long UserId { get; set; }
		public long ProductId { get; set; }
		public int Score { get; set; }

		protected bool Equals(ProductInsightDto other)
		{
			return UserId == other.UserId && ProductId == other.ProductId;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((ProductInsightDto) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (UserId.GetHashCode()*397) ^ ProductId.GetHashCode();
			}
		}
	}
}
