using Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; }
    }
}
