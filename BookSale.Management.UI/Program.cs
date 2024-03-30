using BookSale.Management.DataAccess;
using BookSale.Management.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
var builderRazor = builder.Services.AddRazorPages();
// Add services to the container.

//sau khi chuyển DbContext, IdentityUser và Role qua Infrastructure ở folder Configuration vào một chỗ thì qua lại đây để gọi
builder.Services.RegisterDb(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapper();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorizationGlobal();

//Đăng ký session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);//Hết hạn trong 30p  //Không gọi thì mặc định là 20p
    // Lựa chọn loại lưu trữ session, ví dụ Memory hoặc Redis                                        
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

//Chạy qua đây check xem database có hay chưa, sẽ tạo mới
await app.AutoMigration();

await app.SeedData(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    builderRazor.AddRazorRuntimeCompilation();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();   //Khi gọi phương thức này thì mặc định trỏ đến của các css hay js... là wwwroot

app.UseRouting();

app.UseAuthorization();

app.UseSession();   //Add vào để dùng
app.UseRouting();

//C1: Config route url cho Area Admin or ....
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"); //domain(tên miền)/{Area}/Controller/Action/Id?

//C2: Chỉ định URL khi đã có Area Admin hoặc ...
app.MapAreaControllerRoute(
    name: "AdminRouting",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    ).RequireAuthorization("AuthorizedAdminPolicy");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); //domain(tên miền)/Controller/Action/Id? Mặc định khi khởi chạy

app.MapRazorPages();

app.Run();
