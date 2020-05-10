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
            RuleFor(x => x.UserReference).NotEmpty();
            RuleFor(x => x.UserReference).MaximumLength(100);
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).Matches(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            RuleFor(x => x.Email).MustAsync(IsEmailAlreadyInDatabase);
        }

        private async Task<bool> IsEmailAlreadyInDatabase(string abbreviation, CancellationToken token)
        {
            var user = await _securityService.GetUserByEmail(abbreviation);
            return user != null;
        }
    }
}
