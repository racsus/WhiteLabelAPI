using Core.Entities;
using Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Security
{
    public interface ISecurityService
    {
        Task<List<User>> GetAllUsers();
        Task<User> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(string userReference);
    }
}
