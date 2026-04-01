using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using motorak.dal.Entites;

namespace motorak.pll.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ConfirmEmailModel> _logger;
        private readonly SignInManager<User> _signInManager;

        public ConfirmEmailModel(UserManager<User> userManager, ILogger<ConfirmEmailModel> logger, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code, string returnUrl = null)
        {
            // Log the incoming parameters for debugging
            _logger.LogInformation($"ConfirmEmail called with userId: {userId}, code: {code?.Substring(0, Math.Min(10, code?.Length ?? 0))}...");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                _logger.LogWarning("UserId or code is null/empty");
                StatusMessage = "Invalid confirmation link.";
                return RedirectToPage("/Index");
            }

            // Try to find user with retry mechanism
            User user = null;
            int retryCount = 0;
            const int maxRetries = 3;

            while (user == null && retryCount < maxRetries)
            {
                user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    retryCount++;
                    _logger.LogWarning($"User not found on attempt {retryCount}. UserId: {userId}");

                    if (retryCount < maxRetries)
                    {
                        await Task.Delay(1000); // Wait 1 second before retry
                    }
                }
            }

            if (user == null)
            {
                _logger.LogError($"Unable to load user with ID '{userId}' after {maxRetries} attempts.");
                StatusMessage = "User not found. The confirmation link may be invalid or expired.";
                return RedirectToPage("/Index");
            }

            // Check if email is already confirmed
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                _logger.LogInformation($"Email already confirmed for user {userId}");
                StatusMessage = "Your email is already confirmed.";
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl ?? "/");
            }

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error decoding confirmation code");
                StatusMessage = "Invalid confirmation code format.";
                return RedirectToPage("/Index");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Email confirmed successfully for user {userId}");
                StatusMessage = "Your email has been confirmed successfully!";

                // Automatically sign in the user after confirmation
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl ?? "/");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError($"Failed to confirm email for user {userId}. Errors: {errors}");
                StatusMessage = "Error confirming your email. The link may be invalid or expired.";
                return RedirectToPage("/Index");
            }
        }
    }
}