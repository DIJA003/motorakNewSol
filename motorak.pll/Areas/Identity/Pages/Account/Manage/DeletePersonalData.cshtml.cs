// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using motorak.dal.Entites;
using motorak.DAL.DataBase;

namespace motorak.pll.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly MotorakDbContext _context;

        public DeletePersonalDataModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            MotorakDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Delete all user claims
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (claims.Any())
                    {
                        await _userManager.RemoveClaimsAsync(user, claims);
                    }

                    // 2. Delete all user logins
                    var logins = await _userManager.GetLoginsAsync(user);
                    if (logins.Any())
                    {
                        foreach (var login in logins)
                        {
                            await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                        }
                    }

                    // 3. Delete all user tokens
                    var providers = new[] { "AspNetUserStore" }; // Add custom providers if needed
                    foreach (var provider in providers)
                    {
                        await _userManager.RemoveAuthenticationTokenAsync(user, provider, "RefreshToken");
                        await _userManager.RemoveAuthenticationTokenAsync(user, provider, "Token");
                    }

                    // 4. Remove user from all roles
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, roles);
                    }


                    // 5. Delete custom related data (add your custom entities here)
                    var appointments = await _context.Customers
                        .Where(a => a.UserId == user.Id)
                        .ToListAsync();

                    if (appointments.Any())
                    {
                        _context.Customers.RemoveRange(appointments);
                    }

                    var vehicles = await _context.Mechanics
                        .Where(v => v.UserId == user.Id)
                        .ToListAsync();

                    if (vehicles.Any())
                    {
                        _context.Mechanics.RemoveRange(vehicles);
                    }

                    // Add more custom entity deletions as needed

                    // 6. Finally delete the user
                    var result = await _userManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred deleting user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }

                    // Commit transaction
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    await _signInManager.SignOutAsync();
                    _logger.LogInformation("User with ID '{UserId}' deleted themselves.", user.Id);

                    return Redirect("~/");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error deleting user data");
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting your data. Please try again.");
                    return Page();
                }
            }
        }
    }
}
