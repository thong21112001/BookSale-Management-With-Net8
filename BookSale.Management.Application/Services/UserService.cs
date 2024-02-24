using BookSale.Management.Application.Abstracts;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookSale.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CheckLogin(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                return false;
            }


            //PasswordSignInAsync(user, password, false, false)
            //đối số thứ 1 user: nếu tìm thành công thì truyền vào đây
            //đối số thứ 2 password: truyền password vào để đăng nhập
            //đối số thứ 3 false: là HasRememberMe (tự động lưu) có trong model ở Area Admin
            //đối số thứ 4 false: đếm số lần thất bại của người dùng
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            //Sau khi SignIn trả về SignResult -> SignIn thành công tạo cookies cho chúng ta -> trả về true, thất bại về false

            return result.Succeeded;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
