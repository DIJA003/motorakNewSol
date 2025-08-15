namespace Motorak.Utility
{
    public static class EmailTemplate
    {
        public static string GetEmailConfirmationTemplate(string callbackUrl)
        {
            return $@"
    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #e0e0e0; border-radius: 8px; padding: 20px;'>
        <div style='text-align: center;'>
            <div style='font-size: 32px; color: #1a237e; margin-bottom: 10px;'>
                <span style='display: inline-block; transform: rotate(0deg);'>⚙️</span>
            </div>
            <h2 style='color: #1a237e; margin-top: 0;'>
                Motorak
            </h2>
        </div>
        
        <p>Please confirm your email address to activate your Motorak account.</p>
        
        <div style='text-align: center; margin: 25px 0;'>
            <a href='{callbackUrl}' 
               style='display: inline-block; padding: 12px 24px; background: #1a237e; color: white; 
                      text-decoration: none; border-radius: 4px; font-weight: bold;'>
                Confirm Email
            </a>
        </div>
        
        <p>If you didn't create a Motorak account, you can safely ignore this email.</p>
        
        <div style='margin-top: 30px; padding-top: 15px; border-top: 1px solid #e0e0e0; 
                    color: #757575; font-size: 0.9em;'>
            <p>© {DateTime.Now.Year} Motorak. All rights reserved.</p>
        </div>
    </div>";
        }
    }
}