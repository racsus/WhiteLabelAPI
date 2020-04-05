using Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Base
{
    public interface IReferenceService
    {
        Task<User> CreateReference(User user);
    }
}
