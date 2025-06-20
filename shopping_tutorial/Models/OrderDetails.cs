namespace shopping_tutorial.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string OrderCode { get; set; }
		public long ProductId { get; set; }
		public decimal Price { get; set; } // lấy giá hiện tại không lấy giá từ product 
		public int Quantity  { get; set; }
	}
}
