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
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<MotorakDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

            

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            builder.Services.AddRazorPages();

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
            builder.Services.AddScoped<IServiceReviewService, ServiceReviewSercice>();
            builder.Services.AddScoped<IServiceServicecs, ServiceServices>();

            //emailsender
            builder.Services.AddScoped<IEmailSender, EmailSender>();



            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new CarProfile());
                cfg.AddProfile(new CustomerProfile());
                cfg.AddProfile(new MechanicProfile());
                cfg.AddProfile(new ServiceProfile());
                cfg.AddProfile(new ServiceReviewProfile());
                cfg.AddProfile(new TransactionsProfile());
            });

            //builder.Services.AddAuthentication()  
            //.AddGoogle(options =>
            //{
            //    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            //});
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();



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
