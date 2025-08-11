using Microsoft.EntityFrameworkCore;
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

            //Services
            builder.Services.AddScoped<ICarServicecs, CarService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IMechanicService, MechanicService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IPurchaseService, PurchaseService>();
            builder.Services.AddScoped<IRentService, RentService>();
            builder.Services.AddScoped<IServiceReviewService, ServiceReviewSercice>();
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

            //builder.Services.AddAuthentication()  
            //.AddGoogle(options =>
            //{
            //    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            //});

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MotorakDbContext>();
                    await dal.DataTemp.DbSeeder.SeedAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during database seeding.");
                }
            }

            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    string[] roles = { "Admin", "Mechanic", "Customer" };
            //    foreach (var role in roles)
            //    {
            //        var roleExist = await roleManager.RoleExistsAsync(role);
            //        if (!roleExist)
            //        {
            //            await roleManager.CreateAsync(new IdentityRole(role));
            //        }
            //    }
            //}

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
