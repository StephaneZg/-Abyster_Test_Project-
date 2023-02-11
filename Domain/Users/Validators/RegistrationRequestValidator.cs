
using Abyster_Test_Project.Domain.Users.Dtos;
using FluentValidation;

namespace Abyster_Test_Project.Domain.Users.Validators;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest> {

    public RegistrationRequestValidator(){

        RuleFor(registration => registration.firstName).NotEmpty().WithMessage("First name is required");
        RuleFor(registration => registration.lastName).NotEmpty().WithMessage("Last name is required");
    }
}