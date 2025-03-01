using Microsoft.AspNetCore.Identity;

namespace IdentitySample.Tools;

public class PersianErrorIdentity:IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
           => new IdentityError()
           {
               Code = nameof(DuplicateEmail),
               Description = $"توسط شخص دیگری انتخاب شده است '{email}' ایمیل"
           };

    public override IdentityError DuplicateUserName(string userName)
        => new IdentityError()
        {
            Code = nameof(DuplicateUserName),
            Description = $"توسط شخص دیگری انتخاب شده است '{userName}' نام کاربری"
        };

    public override IdentityError InvalidEmail(string email)
        => new IdentityError()
        {
            Code = nameof(InvalidEmail),
            Description = $"یک ایمیل معتبر نیست '{email}' ایمیل"
        };

    
    public override IdentityError PasswordRequiresDigit()
        => new IdentityError()
        {
            Code = nameof(PasswordRequiresDigit),
            Description = $"رمز عبور باید حداقل دارای یک عدد باشد"
        };


    public override IdentityError PasswordTooShort(int length)
        => new IdentityError()
        {
            Code = nameof(PasswordTooShort),
            Description = $"کاراکتر باشد {length} رمز عبور نباید کمتر از"
        };

    public override IdentityError InvalidUserName(string userName)
        => new IdentityError()
        {
            Code = nameof(InvalidUserName),
            Description = $"معتبر نیست '{userName}' نام کاربری"
        };

}
