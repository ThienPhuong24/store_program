using Microsoft.AspNetCore.Mvc;
using shopping_tutorial.Models;
using shopping_tutorial.Repository;
using System.Security.Claims;

namespace shopping_tutorial.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly DataContext _dataContext;
		public CheckoutController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if(userEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var ordercode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.OrderCode = ordercode;
				orderItem.UserName = userEmail;
				orderItem.Status = 1; // 1 là đơn hàng mới 
				orderItem.CreatedDate = DateTime.Now;
				_dataContext.Add(orderItem);// thêm dữ liệu tạo đơn hàng mới 
				_dataContext.SaveChanges();
				List<CartItemModel> cartitems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach(var cart in cartitems)
				{
					var orderdetails = new OrderDetails();
					orderdetails.UserName = userEmail;
					orderdetails.OrderCode = ordercode;
					orderdetails.ProductId = cart.ProductId;
					orderdetails.Price = cart.Price;
					orderdetails.Quantity = cart.Quantity;
					_dataContext.Add(orderItem);// thêm dữ liệu tạo đơn hàng mới 
					_dataContext.SaveChanges();
				}
				TempData["success"] = "Checkout thành công";
				return RedirectToAction("Index","Cart");


			}
			return View();
		}
	}
}
