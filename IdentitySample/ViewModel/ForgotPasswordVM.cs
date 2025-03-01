using System.ComponentModel.DataAnnotations;

namespace IdentitySample.ViewModel;

public class ForgotPasswordVM
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}