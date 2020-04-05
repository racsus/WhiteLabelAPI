using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Data
{
    public interface IDbSettings
    {
        string ConnectionString { get; set; }
    }
}
