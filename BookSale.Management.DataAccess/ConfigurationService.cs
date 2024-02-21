using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Entities;
using BookSale.Management.Domain.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSale.Management.DataAccess
{
    //Class để tự động Migration khi thực hiện chạy dự án
    public static class ConfigurationService
    {
        public static async Task AutoMigration(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<BookSaleDbContext>();

                //Nếu Database có tồn tại thì bỏ qua -> không thì thực hiện tạo
                //Tuy nhiên trong môi trường dự án thực thì không nên dùng lệnh này
                //appContext.Database.EnsureCreated();


                //Run tất cả các file có trong Migrations đã được tạo
                //appContext.Database.MigrateAsync().Wait();
                await appContext.Database.MigrateAsync();//-> dùng cái này tránh tình trạng deadlock 
            }
        }

        public static async Task SeedData(this WebApplication webApplication, IConfiguration configuration)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                //B1: Khai báo và lấy dữ liệu

                //Class có sẵn của visual UserManager và RoleManager
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // "DefaultUsers": nằm bên file appsettings của UI
                // DefaultUsers: nằm ở Core -> Domain -> Settings
                //Get key trong này GetSection -> sẽ map sang đây Get<DefaultUsers>
                var defaultUser = configuration.GetSection("DefaultUsers")?.Get<DefaultUsers>() ?? new DefaultUsers();

                //Lấy role mặc định khai báo nằm bên file appsettings của UI
                var defaultRole = configuration.GetValue<string>("DefaultRole") ?? "SuperAdmin";


                try
                {
                    //B2: Add role vào database
                    if (!await roleManager.RoleExistsAsync(defaultRole))   //Nếu role rỗng thì insert vào
                    {
                        await roleManager.CreateAsync(new IdentityRole(defaultRole));
                    }

                    //B3: Check acc user
                    var userExits = await userManager.FindByNameAsync(defaultUser.Username);

                    if (userExits == null)  //Nếu user không có ms add vào
                    {
                        //B4: Add User
                        var getUser = new ApplicationUser
                        {
                            UserName = defaultUser?.Username,
                            IsActive = true,
                            AccessFailedCount = 0
                        };

                        var identityUser = await userManager.CreateAsync(getUser, defaultUser.Password);

                        if (identityUser.Succeeded)
                        {
                            //Add role cho user mặc định
                            await userManager.AddToRoleAsync(getUser, defaultRole);

                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
