using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Base
{
    public interface IReferenceGenerator
    {
        string CreateReference(int size);
        string CreateReference(string prefix, int size);
        string CreateReference(string prefix, string countrycode, int size);
    }
}
