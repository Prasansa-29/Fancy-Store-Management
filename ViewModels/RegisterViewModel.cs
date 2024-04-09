


using System.ComponentModel.DataAnnotations;

namespace Fancy.Web.ViewModels
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter username")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}

