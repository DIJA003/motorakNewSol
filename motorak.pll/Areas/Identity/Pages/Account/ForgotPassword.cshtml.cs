// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using motorak.dal.Entites;
using Motorak.Utility;

namespace motorak.pll.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ForgotPasswordModel> _logger;

        public ForgotPasswordModel(UserManager<User> userManager, IEmailSender emailSender, ILogger<ForgotPasswordModel> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user == null)
                    {
                        // Don't reveal that the user does not exist
                        _logger.LogWarning("Password reset attempt for non-existent email: {Email}", Input.Email);
                        return RedirectToPage("./ForgotPasswordConfirmation");
                    }

                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        // Don't reveal that the user's email is not confirmed
                        _logger.LogWarning("Password reset attempt for unconfirmed email: {Email}", Input.Email);
                        return RedirectToPage("./ForgotPasswordConfirmation");
                    }

                    // Generate password reset token
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code, email = Input.Email },
                        protocol: Request.Scheme);

                    // Create a proper HTML email template for password reset
                    var emailBody = CreatePasswordResetEmailTemplate(user.Name, callbackUrl);

                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "Reset Your Motorak Password",
                        emailBody);

                    _logger.LogInformation("Password reset email sent to: {Email}", Input.Email);
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while sending password reset email to: {Email}", Input.Email);
                    // Still redirect to confirmation page to not reveal the error to potential attackers
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
            }

            return Page();
        }

        private string CreatePasswordResetEmailTemplate(string userName, string resetUrl)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Reset Your Password</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; margin: 0; padding: 20px; background-color: #f4f4f4; }}
        .container {{ max-width: 600px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }}
        .header {{ text-align: center; margin-bottom: 30px; }}
        .logo {{ font-size: 28px; font-weight: bold; color: #007bff; margin-bottom: 10px; }}
        .content {{ margin-bottom: 30px; }}
        .button {{ display: inline-block; padding: 15px 25px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px; font-weight: bold; margin: 20px 0; }}
        .button:hover {{ background-color: #0056b3; }}
        .footer {{ text-align: center; color: #666; font-size: 14px; margin-top: 30px; border-top: 1px solid #eee; padding-top: 20px; }}
        .warning {{ background-color: #fff3cd; color: #856404; padding: 15px; border-radius: 5px; margin: 20px 0; border-left: 4px solid #ffc107; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='logo'>🚗 Motorak</div>
            <h2>Password Reset Request</h2>
        </div>
        
        <div class='content'>
            <p>Hello {userName},</p>
            
            <p>We received a request to reset your password for your Motorak account. If you made this request, click the button below to reset your password:</p>
            
            <div style='text-align: center;'>
                <a href='{HtmlEncoder.Default.Encode(resetUrl)}' class='button'>Reset My Password</a>
            </div>
            
            <div class='warning'>
                <strong>⚠️ Security Notice:</strong>
                <ul>
                    <li>This link will expire in 24 hours for security reasons</li>
                    <li>If you didn't request this password reset, please ignore this email</li>
                    <li>Never share this link with anyone</li>
                </ul>
            </div>
            
            <p>If the button above doesn't work, you can copy and paste the following link into your browser:</p>
            <p style='word-break: break-all; background-color: #f8f9fa; padding: 10px; border-radius: 5px; font-family: monospace;'>
                {HtmlEncoder.Default.Encode(resetUrl)}
            </p>
        </div>
        
        <div class='footer'>
            <p>This email was sent from Motorak - Your Trusted Car Platform</p>
            <p>If you have any questions, please contact our support team.</p>
            <p style='font-size: 12px;'>© {DateTime.Now.Year} Motorak. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}