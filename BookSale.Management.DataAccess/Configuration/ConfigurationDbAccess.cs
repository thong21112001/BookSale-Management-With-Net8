using BookSale.Management.DataAccess.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSale.Management.DataAccess.Configuration
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
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<BookSaleDbContext>();
        }
    }
}
