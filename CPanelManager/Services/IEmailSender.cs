#region Using

using CPanelManager.Models;
using System.Threading.Tasks;

#endregion

namespace CPanelManager.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        void SendEmail(Message message);
    }
}