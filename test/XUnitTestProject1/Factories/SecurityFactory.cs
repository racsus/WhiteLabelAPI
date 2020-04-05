using Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteLabelAPI.Test.Factories
{
    public static class SecurityFactory
    {
        public static User CreateUserModel()
        {
            return new User
            {
                UserId = 1,
                Name = "Test",
                Department = "Test Departament",
                Email = "test@test.net",
                Responsable = "My Boss",
                LanguageId = 1,
                Extension = "123",
                Telephone = "+34965999999",
                Job = "Developer",
                UserReference = "123edIy56T"
            };
        }

    }
}
