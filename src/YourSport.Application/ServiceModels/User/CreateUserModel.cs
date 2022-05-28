using System.Globalization;
using System.Resources;
using FluentValidation;

namespace YourSport.Application.ServiceModels.User;

public class CreateUserModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Key { get; set; }
    public int PhoneNumber { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string StateInitials { get; set; }
}

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
        
        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage(resourceSet?.GetString("USER-NAME_EMPTY"))
            .MaximumLength(256)
            .WithMessage(string.Format(resourceSet?.GetString("USER-NAME_MAX_SIZE")!, 256));

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage(resourceSet?.GetString("USER-EMAIL_EMPTY"))
            .EmailAddress()
            .WithMessage(resourceSet?.GetString("USER-EMAIL_INVALID"));

        RuleFor(u => u.Key)
            .NotEmpty()
            .WithMessage(resourceSet?.GetString("USER-KEY_EMPTY"));
        
        RuleFor(c => c.State)
            .NotEmpty()
            .WithMessage(resourceSet?.GetString("STATE-NAME_EMPTY"))
            .MaximumLength(256)
            .WithMessage(string.Format(resourceSet?.GetString("STATE-NAME_MAX_SIZE")!, 256));
        
        RuleFor(c => c.StateInitials)
            .NotEmpty()
            .WithMessage(resourceSet?.GetString("STATE-INITIALS_EMPTY"))
            .MaximumLength(16)
            .WithMessage(string.Format(resourceSet?.GetString("STATE-INITIALS_MAX_SIZE")!, 16));
    }
}