using Core.Entities.Security;
using Core.Interfaces.Base;
using Core.Interfaces.Security;
using Core.Services;
using WhiteLabelAPI.Test.Factories;
using WhiteLabelAPI.Test.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Core.Services
{
    public class Users
    {
        private User _user = SecurityFactory.CreateUserModel();
        private SecurityService _securityService;

        public Users()
        {
            _securityService =
                new SecurityService(SecurityMocks.GetIUserRepositoryMock(_user).Object,
                SecurityMocks.GetIReferenceServiceMock(_user).Object
            );
        }

        [Fact]
        public async Task AddUser()
        {
            var res = await _securityService.AddUser(_user);
            Assert.True(res!=null);
        }

        [Fact]
        public async Task UpdateUser()
        {
            var res = await _securityService.UpdateUser(_user);
            Assert.True(res);
        }

        [Fact]
        public async Task DeleteUser()
        {
            var res = await _securityService.DeleteUser(_user.UserReference);
            Assert.True(res);
        }

        [Fact]
        public async Task GetAllUsers()
        {
            var res = await _securityService.GetAllUsers();
            Assert.True(res.Count > 0);
        }
    }
}
