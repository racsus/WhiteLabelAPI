using Core.Entities.Security;
using Core.Interfaces.Security;
using Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Security
{
    public class UserRepository : IUserRepository
    {
        private readonly IWhiteLabelAPIContext _cleanArchitectureContext;
        public UserRepository(IWhiteLabelAPIContext cleanArchitectureContext)
        {
            _cleanArchitectureContext = cleanArchitectureContext;
        }

        public async Task<User> CreateUser(User user)
        {
            _cleanArchitectureContext.Users.Add(user);
            if (await _cleanArchitectureContext.SaveChangesAsync() > 0)
            {
                return await GetUserByReference(user.UserReference);
            }

            return null;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _cleanArchitectureContext.Users.Update(user);
            return await _cleanArchitectureContext.SaveChangesAsync() > 0;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _cleanArchitectureContext.Users
                .Where(x => x.DateDeleted == null)
                .ToListAsync();
        }

        public async Task<User> GetUserByReference(string userReference)
        {
            return await _cleanArchitectureContext.Users
                .AsNoTracking()
                .Where(x => x.DateDeleted == null && x.UserReference == userReference).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _cleanArchitectureContext.Users
                .AsNoTracking()
                .Where(x => x.DateDeleted == null && x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteUser(string userReference)
        {
            User user = (await GetUserByReference(userReference));
            user.SoftDelete();
            return await UpdateUser(user);
        }

        public async Task<bool> IsReferenceExists(string userReference)
        {
            User user = await _cleanArchitectureContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.UserReference == userReference);

            return user != null;
        }

    }
}
