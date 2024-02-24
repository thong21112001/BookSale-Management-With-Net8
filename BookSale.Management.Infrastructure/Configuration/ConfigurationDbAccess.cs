using BookSale.Management.Application;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.Services;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.DataAccess.Repository;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSale.Management.Infrastructure.Configuration
{
    public static class ConfigurationDbAccess
    {
        public static void RegisterDb(this IServiceCollection services, IConfiguration configuration)
        {
            //Chuyển từ file Program.cs qua để đăng ký dbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<BookSaleDbContext>(options => options.UseSqlServer(connectionString));

            //Đăng ký IdentityUser và Role
            services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<BookSaleDbContext>()
                            .AddDefaultTokenProviders();

            // Đăng ký UserManager
            services.AddScoped<UserManager<ApplicationUser>>();

            //Đăng ký cookies
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "BookSaleManagementCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                //Nếu chưa chứng thực người dùng thì sẽ bị đá ra ngoài login
                options.LoginPath = "/admin/authentication/login";  //Thêm [Authorize] ở trong các Controller Admin
            });
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<PasswordHasher<ApplicationUser>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService,UserService>();
        }
    }
}
