using Core.DTO.Security;
using Core.Interfaces.Security;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Validators.Security
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        private readonly ISecurityService _securityService;
        public UserDTOValidator(ISecurityService securityService)
        {
            _securityService = securityService;

            //Required Checks
            RuleFor(x => x.UserReference).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();

            //Max Length
            RuleFor(x => x.UserReference).MaximumLength(100);
            RuleFor(x => x.Name).MaximumLength(100);

            //Regular Expression Checks
            RuleFor(x => x.Email).Matches(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");

            //Database Checks
            RuleFor(x => x.Email).MustAsync(IsEmailAlreadyInDatabase).WithMessage("Email already exists in database."); ;
        }

        private async Task<bool> IsEmailAlreadyInDatabase(string abbreviation, CancellationToken token)
        {
            var user = await _securityService.GetUserByEmail(abbreviation);
            return user == null;
        }
    }
}
