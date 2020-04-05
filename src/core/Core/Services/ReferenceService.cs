using Core.Entities.Security;
using Core.Interfaces.Base;
using Core.Interfaces.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IReferenceGenerator _referenceGenerator;
        private readonly IUserRepository _userRepository;

        public ReferenceService(IReferenceGenerator referenceGenerator, IUserRepository userRepository)
        {
            _referenceGenerator = referenceGenerator;
            _userRepository = userRepository;
        }

        public async Task<User> CreateReference(User user)
        {
            user.CreateReference(_referenceGenerator);

            while (await _userRepository.IsReferenceExists(user.UserReference))
            {
                user.CreateReference(_referenceGenerator);
            }
            return user;
        }
    }
}
