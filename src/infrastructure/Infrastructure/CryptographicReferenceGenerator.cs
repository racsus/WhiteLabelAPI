using Core.Interfaces.Base;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure
{
    public class CryptographicReferenceGenerator : IReferenceGenerator
    {
        public string CreateReference(int size)
        {
            return CreateReference(string.Empty, string.Empty, size);
        }

        public string CreateReference(string prefix, int size)
        {
            return CreateReference(prefix, string.Empty, size);
        }

        public string CreateReference(string prefix, string countrycode, int size)
        {
            const int byteSize = 0x100;
            // NOTE: Lowecase L, uppercase I, o, O and 0 (zero) all removed to prevent confusion and customers typing the wrong characters.
            char[] chars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789".ToCharArray();
            char[] allowedCharSet = new HashSet<char>(chars).ToArray();

            using (RandomNumberGenerator cryptoProvider = RandomNumberGenerator.Create())
            {
                var result = new StringBuilder();
                var buffer = new byte[128];

                while (result.Length < size)
                {
                    cryptoProvider.GetBytes(buffer);

                    for (var i = 0; i < buffer.Length && result.Length < size; ++i)
                    {
                        int outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);

                        if (outOfRangeStart <= buffer[i])
                        {
                            continue;
                        }

                        result.Append(allowedCharSet[buffer[i] % allowedCharSet.Length]);
                    }
                }

                return $"{prefix}{countrycode}{result}";
            }
        }
    }
}
