using Core.Entities.Security;
using Core.Interfaces.Base;
using Core.Interfaces.Security;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhiteLabelAPI.Test.Mocks
{
    public static class SecurityMocks
    {
        public static Mock<IUserRepository> GetIUserRepositoryMock(User user)
        {
            var mockDependency = new Mock<IUserRepository>();

            mockDependency.Setup(x => x.CreateUser(user))
                .Returns(Task.FromResult(user));
            mockDependency.Setup(x => x.UpdateUser(user))
                .Returns(Task.FromResult(true));
            mockDependency.Setup(x => x.DeleteUser(user.UserReference))
                .Returns(Task.FromResult(true));
            mockDependency.Setup(x => x.GetAllUsers())
                .Returns(Task.FromResult(new List<User> { user }));
            mockDependency.Setup(x => x.GetUserByReference(user.UserReference))
                .Returns(Task.FromResult(user));

            return mockDependency;
        }

        public static Mock<IReferenceService> GetIReferenceServiceMock(User user)
        {
            var mockDependency = new Mock<IReferenceService>();

            mockDependency.Setup(x => x.CreateReference(user))
                .Returns(Task.FromResult(user));

            return mockDependency;
        }
    }
}
