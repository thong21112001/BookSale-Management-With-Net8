using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.Services;
using BookSale.Management.DataAccess.Dapper;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.DataAccess.Repository;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                //Nếu chưa chứng thực người dùng thì sẽ bị đá ra ngoài login
                options.LoginPath = "/admin/authentication/login";  //Thêm [Authorize] ở trong các Controller Admin
                options.SlidingExpiration = true; //Nếu còn ở hệ thống sẽ gia hạn thêm cookies
            });

            //Đăng ký khi sử dụng khoá tài khoản
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 3;    //Bao nhiêu lần đc login false
            });

            //Đăng ký khi check pass của user
            services.Configure<IdentityOptions>(options =>
            {
                //Check Password
                options.Password.RequireDigit = true;   //Yêu cầu có kí tự số
                options.Password.RequireLowercase = true;   //Yêu cầu có chữ thường
                options.Password.RequireUppercase = true;   //Yêu cầu có chữ hoa
                options.Password.RequireNonAlphanumeric = true; //Yêu cầu có kí tự đặc biệt
                options.Password.RequiredLength = 8;    //Độ dài pass từ 8
                options.Password.RequiredUniqueChars = 5;   //Ít nhất 5 kí tự khác nhau

                //Check email
                options.User.RequireUniqueEmail = true; // Yêu cầu email phải là duy nhất
            });
        }

        //Cấu hình đăng ký dịch dụ, repository, dapper
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<PasswordHasher<ApplicationUser>>();
			services.AddTransient<ISQLQueryHandler, SQLQueryHandler>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IVnPayService, VnPayService>();
            services.AddTransient<IErrorMessageService, ErrorMessageService>();



            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IUserAddressService, UserAddressService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        
        //Xác thực người dùng với Authorize ở trên mỗi controller, làm ngắn gọn code, không lặp
        public static void AddAuthorizationGlobal(this IServiceCollection services)
        {
            var authorizedAdmin = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthorizedAdminPolicy", authorizedAdmin);
            });
        }
    }
}
