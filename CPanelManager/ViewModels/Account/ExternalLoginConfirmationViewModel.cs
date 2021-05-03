#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace CPanelManager.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}