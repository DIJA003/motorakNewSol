using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using motorak.dal.DataTemp;
using motorak.dal.Entites;
using motorak.dal.Repo.Abstractions;
using motorak.dal.Repo.Implementations;
using motorak.DAL.DataBase;
using Motorak.BLL.Mapper.CarMapping;
using Motorak.BLL.Mapper.CustomerMapping;
using Motorak.BLL.Mapper.MechanicMappin;
using Motorak.BLL.Mapper.ServiceMapping;
using Motorak.BLL.Mapper.ServiceReviewMapping;
using Motorak.BLL.Mapper.TransactionMapping;
using Motorak.BLL.Services.Abstractions;
using Motorak.BLL.Services.Implementations;
using Motorak.DAl.Repo.Abstractions;
using Motorak.DAl.Repo.Implementations;
using Motorak.DAL.Repo.Abstractions;
using Motorak.DAL.Repo.Implementations;
using Motorak.Utility;

namespace motorak.pll
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("defaultConnection");
            builder.Services.AddDbContext<MotorakDbContext>(options =>
                options.UseSqlServer(connectionString));

            

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<MotorakDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            builder.Services.AddRazorPages();

            //emailsender
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Repos
            builder.Services.AddScoped<ICarRebo, CarRebo>();
            builder.Services.AddScoped<ICustomerRebo, CustomerRebo>();
            builder.Services.AddScoped<IMechanicRebo, MechanicRebo>();
            builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
            builder.Services.AddScoped<IPurchaseRepo, PurchaseRepo>();
            builder.Services.AddScoped<IRentRepo, RentRepo>();
            builder.Services.AddScoped<IServiceReviewRepo, ServiceReviewRepo>();
            builder.Services.AddScoped<IServiceRepo, ServiceRepo>();
            builder.Services.AddScoped<ICartRepo,CartRepo>();

            //Services
            builder.Services.AddScoped<ICarServicecs, CarService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IMechanicService, MechanicService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IPurchaseService, PurchaseService>();
            builder.Services.AddScoped<IRentService, RentService>();
            builder.Services.AddScoped<IServiceReviewService, ServiceReviewService>();
            builder.Services.AddScoped<IServiceServicecs, ServiceServices>();

            


            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new CarProfile());
                cfg.AddProfile(new CustomerProfile());
                cfg.AddProfile(new MechanicProfile());
                cfg.AddProfile(new ServiceProfile());
                cfg.AddProfile(new ServiceReviewProfile());
                cfg.AddProfile(new TransactionsProfile());
            });

            builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = "367275960270-k61vvo1n6opfak2b94km9ki769d23o90.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });
            builder.Services.AddAuthentication()
            .AddFacebook(options =>
            {
                options.AppId = "866662105870365";
                options.AppSecret = "1273d4869397bd9f897cc371af7cc752";
            });


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MotorakDbContext>();
                    await DbSeeder.SeedAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
