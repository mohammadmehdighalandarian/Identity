using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace IdentitySample.ViewModel;

public class RegisterVM
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [Phone]
    public string Phone { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

}