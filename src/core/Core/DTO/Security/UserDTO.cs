using Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.Security
{
    public class UserDTO
    {
        public string UserReference { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int ProductionCentreId { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }
        public string Telephone { get; set; }
        public string Extension { get; set; }
        public string Responsable { get; set; }
        public bool Active { get; set; }
        public int CustomerId { get; set; }
        public int LanguageId { get; set; }
    }
}
