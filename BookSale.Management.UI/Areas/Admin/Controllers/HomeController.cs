using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    //domain/Admin/Home/Index
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
		[Breadscrum("Home")]
		public IActionResult Index()
        {
            return View();
        }
    }
}
