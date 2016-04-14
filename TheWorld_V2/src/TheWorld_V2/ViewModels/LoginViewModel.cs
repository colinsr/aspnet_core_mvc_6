using System.ComponentModel.DataAnnotations;

namespace TheWorld_V2.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; } 
    }
}