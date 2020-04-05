using Core.Entities.Base;
using Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Security
{
    public class User : LifeTimeBase
    {
        public int UserId { get; set; }
        public string UserReference { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }
        public string Telephone { get; set; }
        public string Extension { get; set; }
        public string Responsable { get; set; }
        public bool Active { get; set; }
        public int? CustomerId { get; set; }
        public int LanguageId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public User CreateReference(IReferenceGenerator referenceGenerator)
        {
            UserReference = referenceGenerator.CreateReference(Constants.Settings.ReferenceLength);

            return this;
        }
    }
}
