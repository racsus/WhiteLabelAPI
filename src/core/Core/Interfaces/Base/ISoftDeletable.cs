using Core.Entities;
using Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Base
{
    public interface ISoftDeletable
    {
        DateTime? DateDeleted { get; }
        string DeletedBy { get;  }

        void SoftDelete();
    }
}
