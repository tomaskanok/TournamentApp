using System.ComponentModel.DataAnnotations;
using TournamentApp.App_LocalResources;

namespace TournamentApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Jméno")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nynější heslo")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musí být aspoň {2} znaků dlouhé.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nové heslo")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdit nové heslo")]
        [Compare("NewPassword", ErrorMessage = "Hesla nejsou stejné.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Display(Name = "Zapamatovat jsi mě?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Jméno")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musí být aspoň {2} znaků dlouhé.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdit nové heslo")]
        [Compare("Password", ErrorMessage = "Hesla nejsou stejné.")]
        public string ConfirmPassword { get; set; }
    }
}
