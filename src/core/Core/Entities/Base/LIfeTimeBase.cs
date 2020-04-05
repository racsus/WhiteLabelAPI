using Core.Entities.Security;
using Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Base
{
    public class LifeTimeBase : ISoftDeletable
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string DeletedBy { get; set; }

        public LifeTimeBase()
        {
            DateCreated = DateTime.UtcNow;
            DateUpdated = DateTime.UtcNow;
            DateDeleted = null;
        }

        public void MarkUpdated()
        {
            DateUpdated = DateTime.UtcNow;
        }

        public void SoftDelete()
        {
            DateUpdated = DateTime.UtcNow;
            DateDeleted = DateTime.UtcNow;
        }

        public bool IsActive => DateDeleted == null;
    }
}
