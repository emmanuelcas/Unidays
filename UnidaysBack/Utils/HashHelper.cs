using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace UnidaysBack.Utils
{
    public static class HashHelper
    {
        public static string GetStringHash(string input)
        {
            var bins = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bins)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }


    }
}
