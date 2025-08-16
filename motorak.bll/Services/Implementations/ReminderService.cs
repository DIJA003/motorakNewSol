using motorak.bll.Services.Abstractions;

namespace Motorak.BLL.Services
{
    public class ReminderService : IReminderService
    {
        public void ShowReminder()
        {
            Console.WriteLine($"Reminder: Check for new cars! {DateTime.Now}");
        }
    }
}