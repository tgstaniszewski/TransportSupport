using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityExample.Models
{
    public class RegisterViewModel
    {
		[Required]
		public string Login { get; set; }

		[Required]
        [DisplayName("Hasło")]
        [DataType(DataType.Password)]
	    public string Password { get; set; }

	    [DataType(DataType.Password)]
        [DisplayName("Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być identyczne")]
	    public string RetypePassword { get; set; }

		[Required]
        [DisplayName("Adres email")]
        [EmailAddress]
	    public string Email { get; set; }
    }
}
