#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace CPanelManager.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}