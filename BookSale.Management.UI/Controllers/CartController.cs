using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class CartController : Controller
    {
        private static string CartSessionName = "CartSession";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CartModel cartModel)
        {
            try
            {
                //Nếu mà nó null sẽ khởi tạo một list cart mới
                var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName) ?? new List<CartModel>();

                if (!carts.Any())
                {
                    carts.Add(cartModel);
                }
                else
                {
                    //Kiểm tra xem codebook có tồn tại trong giỏ không
                    var cartExist = carts.FirstOrDefault(x => x.CodeBook == cartModel.CodeBook);

                    if (cartExist is null)  //Không thì tiến hành add book vào
                    {
                        carts.Add(cartModel);
                    }
                    else//Có thì tiến hành tăng số lượng book
                    {
                        cartExist.Quantity += cartModel.Quantity;
                    }
                }

                //Cập nhập session
                HttpContext.Session.Set(CartSessionName, carts);

                //Trả về số lượng sách trong giỏ
                return Json(carts.Count);
            }
            catch (Exception)
            {
                return Json(-1);
            }
        }
    }
}
