using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
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

        public async Task<ResponseModel> CheckLogin(string username, string password, bool hasRememberMe)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Username hoặc Password sai"

                };
            }

            //PasswordSignInAsync(user, password, false, false)
            //đối số thứ 1 user: nếu tìm thành công thì truyền vào đây
            //đối số thứ 2 password: truyền password vào để đăng nhập
            //đối số thứ 3 false -> hasRememberMe: là HasRememberMe (tự động gia hạn cookies) có trong model ở Area Admin
            //đối số thứ 4 false: đếm số lần thất bại của người dùng
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: hasRememberMe, lockoutOnFailure: true);
            //Sau khi SignIn trả về SignResult -> SignIn thành công tạo cookies cho chúng ta -> trả về true, thất bại về false

            //Thêm thời gian khoá thì vào ConfigurationDbAccess để xử lý
            if (result.IsLockedOut)
            {
                var remainingLockout = user.LockoutEnd.Value - DateTimeOffset.UtcNow;

                return new ResponseModel
                {
                    Status = false,
                    Message = $"Tài khoản bị khoá. Hãy thử lại trong {Math.Round(remainingLockout.TotalMinutes)} phút"

                };
            }

            if (result.Succeeded)
            {
                //Khi người dùng login sai 2 lần nhưng lần 3 đúng thì sẽ xoá 2 lần lỗi, reset lại
                if (user.AccessFailedCount > 0)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                }

                return new ResponseModel
                {
                    Status = true
                };
            }

            return new ResponseModel
            {
                Status = false,
                Message = "Username hoặc Password sai"
            };
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
