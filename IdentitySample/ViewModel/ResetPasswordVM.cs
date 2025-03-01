using System.ComponentModel.DataAnnotations;

namespace IdentitySample.ViewModel;

public class ResetPasswordVM
{
    [Required]
    [DataType(dataType:DataType.Password)]
    [Display(Name =("New Password"))]
    public string Password { get; set; }
    [Required]
    [DataType(dataType: DataType.Password)]
    [Display(Name = ("Confirm Password"))]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Token { get; set; }
}