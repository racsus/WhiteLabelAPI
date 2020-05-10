using Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Security
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<List<User>> GetAllUsers();
        Task<bool> DeleteUser(string userReference);
        Task<User> GetUserByReference(string userReference);
        Task<User> GetUserByEmail(string email);
        Task<bool> IsReferenceExists(string userReference);
    }
}
