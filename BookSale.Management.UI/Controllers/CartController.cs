using BookSale.Management.Application.Abstracts;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BookSale.Management.UI.Controllers
{
    public class CartController : Controller
    {
        private static string CartSessionName = "CartSession";
        private readonly IBookService _bookService;

        public CartController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName);

            if (carts is not null)
            {
                var getCodes = carts.Select(x => x.CodeBook).ToArray();

                var books = await _bookService.GetListBookByCode(getCodes);

                books = books.Select(book =>
                {
                    var item = carts.FirstOrDefault(x => x.CodeBook == book.Code);

                    if (item is not null)
                    {
                        book.Quantity = item.Quantity;
                    }

                    return book;
                });

                return View(books);
            }

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

        //Lấy số lượng item trong giỏ hàng
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName);
            return Json(carts?.Count ?? 0);
        }

        //Lấy giá tiền của book
        private async Task<decimal> GetBookPriceAsync(string codeBook)
        {
            var book = await _bookService.GetBookByCode(codeBook);
            return Convert.ToDecimal(book.Price);
        }

        //Cập nhập số lượng sản phẩm trong giỏ hàng
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(string codeBook, string operation)
        {
            try
            {
                // Lấy danh sách giỏ hàng từ Session
                var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName) ?? new List<CartModel>();

                // Tìm sản phẩm cần cập nhật
                var cartItem = carts.FirstOrDefault(x => x.CodeBook == codeBook);

                if (cartItem != null)
                {
                    decimal cartTotal = 0;

                    if (operation == "plus")
                    {
                        cartItem.Quantity++;
                    }
                    else if (operation == "minus")
                    {
                        cartItem.Quantity--;
                        if (cartItem.Quantity <= 0)
                        {
                            carts.Remove(cartItem);
                            // Cập nhật Session
                            HttpContext.Session.Set(CartSessionName, carts);

                            //Tính tổng giá tiền trong giỏ hàng
                            foreach (var item in carts)
                            {
                                var bookPrice = await GetBookPriceAsync(item.CodeBook);
                                cartTotal += item.Quantity * bookPrice;
                            }
                            
                            // Trả về kết quả để cập nhật giao diện
                            return Json(new
                            {
                                success = true,
                                removeItem = true,
                                codeBook = codeBook,
                                itemCartTotal = carts.Count(),
                                cartTotal = cartTotal.ToString("C", CultureInfo.GetCultureInfo("vi-VN"))
                            });
                        }
                    }

                    // Cập nhật Session
                    HttpContext.Session.Set(CartSessionName, carts);

                    // Tính toán giá tiền mới
                    var book = await _bookService.GetBookByCode(codeBook);
                    var itemTotal = cartItem.Quantity * book.Price;

                    //Tính tổng giá tiền trong giỏ hàng
                    foreach (var item in carts)
                    {
                        var bookPrice = await GetBookPriceAsync(item.CodeBook);
                        cartTotal += item.Quantity * bookPrice;
                    }

                    // Trả về kết quả
                    return Json(new
                    {
                        success = true,
                        quantity = cartItem.Quantity,
                        itemCartTotal = carts.Count(),
                        itemTotal = itemTotal.ToString("C", CultureInfo.GetCultureInfo("vi-VN")),
                        cartTotal = cartTotal.ToString("C", CultureInfo.GetCultureInfo("vi-VN"))
                    });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string codeBook)
        {
            try
            {
                var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName);

                if (carts != null)
                {
                    var cartItem = carts.FirstOrDefault(x => x.CodeBook == codeBook);
                    decimal cartTotal = 0;
                    if (cartItem != null)
                    {
                        carts.Remove(cartItem);
                        // Cập nhật Session
                        HttpContext.Session.Set(CartSessionName, carts);

                        //Tính tổng giá tiền trong giỏ hàng
                        foreach (var item in carts)
                        {
                            var bookPrice = await GetBookPriceAsync(item.CodeBook);
                            cartTotal += item.Quantity * bookPrice;
                        }

                        // Trả về kết quả để cập nhật giao diện
                        return Json(new
                        {
                            success = true,
                            removeItem = true,
                            codeBook = codeBook,
                            itemCartTotal = carts.Count(),
                            cartTotal = cartTotal.ToString("C", CultureInfo.GetCultureInfo("vi-VN"))
                        });
                    }
                }

                return Json(new { success = false });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

    }
}
