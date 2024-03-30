﻿using BookSale.Management.Application.Abstracts;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName);
            return Json(carts?.Count ?? 0);
        }
    }
}
