using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using motorak.dal.Entites;
using Motorak.Utility;

namespace motorak.dal.DataTemp
{
    public static class AdminLogins
    {
        public static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {

                if (!await roleManager.RoleExistsAsync(Seed.Role_Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(Seed.Role_Admin));
                }

                var adminEmail = "admin@motorak.com";
                var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

                if (existingAdmin == null)
                { 
                    var adminUser = new User
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        Name = "System Administrator",
                        EmailConfirmed = true, 
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        CreatedAt = DateTime.Now,
                        PhoneNumber = "1234567890"
                    };


                    var adminPassword = "Admin@123456";
                    var result = await userManager.CreateAsync(adminUser, adminPassword);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, Seed.Role_Admin);
                    }
                    else
                    {
                        Console.WriteLine("Failed to create admin user:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"- {error.Description}");
                        }
                    }
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(existingAdmin, Seed.Role_Admin))
                    {
                        await userManager.AddToRoleAsync(existingAdmin, Seed.Role_Admin);
                        Console.WriteLine($"Added Admin role to existing user: {adminEmail}");
                    }

                    Console.WriteLine($"Admin user already exists: {adminEmail}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding admin: {ex.Message}");
                throw;
            }
        }
    }
}
