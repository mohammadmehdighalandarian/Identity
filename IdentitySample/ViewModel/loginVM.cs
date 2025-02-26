using System.ComponentModel.DataAnnotations;

namespace IdentitySample.ViewModel;

public class loginVM
{
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(dataType:DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Display(Name = "Remember Me?")]
    public bool RememberMe { get; set; }

}