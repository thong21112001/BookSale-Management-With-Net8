using BookSale.Management.Application.Abstracts;
using BookSale.Management.UI.Helpers;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BookSale.Management.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookService _bookService;

        public CartController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var carts = CartHelper.GetCartItems(HttpContext.Session);

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
                CartHelper.AddToCart(HttpContext.Session, cartModel);
                return Json(CartHelper.GetCartItems(HttpContext.Session).Count);
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
            return Json(CartHelper.GetCartItems(HttpContext.Session).Count);
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
                decimal cartTotal = 0;
                int newQuantity;
                double itemTotal;
                var book = await _bookService.GetBookByCode(codeBook);

                if (operation == "plus")
                {
                    newQuantity = CartHelper.IncrementQuantity(HttpContext.Session, codeBook);

                    // Tính toán giá tiền mới của mặt hàng
                    itemTotal = newQuantity * book.Price;

                    var carts = CartHelper.GetCartItems(HttpContext.Session);
                    //Tính tổng giá tiền trong giỏ hàng
                    foreach (var item in carts)
                    {
                        var bookPrice = await GetBookPriceAsync(item.CodeBook);
                        cartTotal += item.Quantity * bookPrice;
                    }

                    return Json(new
                    {
                        success = true,
                        quantity = newQuantity,
                        itemCartTotal = carts.Count(),
                        itemTotal = itemTotal.ToString("C", CultureInfo.GetCultureInfo("vi-VN")),
                        cartTotal = cartTotal.ToString("C", CultureInfo.GetCultureInfo("vi-VN"))
                    });
                }
                else if (operation == "minus")
                {
                    newQuantity = CartHelper.DecrementQuantity(HttpContext.Session, codeBook);
                    bool itemDelete = newQuantity == 0;

                    itemTotal = newQuantity * book.Price;

                    var carts = CartHelper.GetCartItems(HttpContext.Session);
                    //Tính tổng giá tiền trong giỏ hàng
                    foreach (var item in carts)
                    {
                        var bookPrice = await GetBookPriceAsync(item.CodeBook);
                        cartTotal += item.Quantity * bookPrice;
                    }

                    return Json(new
                    {
                        success = true,
                        removeItem = itemDelete,
                        quantity = newQuantity,
                        codeBook = codeBook,
                        itemCartTotal = carts.Count(),
                        itemTotal = itemTotal.ToString("C", CultureInfo.GetCultureInfo("vi-VN")),
                        cartTotal = cartTotal.ToString("C", CultureInfo.GetCultureInfo("vi-VN"))
                    });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
            return Json(new { success = false });
        }

        //Nút xoá sản phẩm khỏi giỏ hàng
        [HttpPost]
        public async Task<IActionResult> Delete(string codeBook)
        {
            try
            {
                CartHelper.RemoveFromCart(HttpContext.Session, codeBook);

                var carts = CartHelper.GetCartItems(HttpContext.Session);
                decimal cartTotal = 0;

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
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }
}