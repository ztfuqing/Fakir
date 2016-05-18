using System.ComponentModel.DataAnnotations;

namespace Fakir.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}