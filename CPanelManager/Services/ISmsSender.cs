#region Using

using System.Threading.Tasks;

#endregion

namespace CPanelManager.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}