using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Entities.Utility
{
    public static class AuthenticationUtility
    {
        public static string CalculateStringHash(string Password)
        {
            var passwordBytes = ASCIIEncoding.ASCII.GetBytes(Password);
            byte[] PasswordHash = new MD5CryptoServiceProvider().ComputeHash(passwordBytes);
            return  ByteArrayToString(PasswordHash);
        }

        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
