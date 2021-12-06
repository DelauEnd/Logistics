using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Utility
{
    public static class AuthenticationUtility
    {
        public static string CalculateStringHash(string Password)
        {
            var passwordBytes = ASCIIEncoding.UTF8.GetBytes(Password);
            var provider = new MD5CryptoServiceProvider();
            byte[] PasswordHash = provider.ComputeHash(passwordBytes);
            return ByteArrayToString(PasswordHash);
        }

        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder outString = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                outString.Append(arrInput[i].ToString("X2"));
            }
            return outString.ToString();
        }
    }
}
