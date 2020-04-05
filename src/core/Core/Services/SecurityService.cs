using Core.Entities.Security;
using Core.Interfaces.Base;
using Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReferenceService _referenceService;

        public SecurityService(IUserRepository userRepository, 
            IReferenceService referenceService)
        {
            _userRepository = userRepository;
            _referenceService = referenceService;
        }

        public async Task<User> AddUser(User user)
        {
            user = await _referenceService.CreateReference(user);
            return await _userRepository.CreateUser(user);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public async Task<bool> DeleteUser(string userReference)
        {
            return await _userRepository.DeleteUser(userReference);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
    }
}
