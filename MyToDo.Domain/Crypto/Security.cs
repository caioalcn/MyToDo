using System;
using System.Collections.Generic;
using System.Text;
using Crypt = BCrypt.Net.BCrypt;

namespace MyToDo.Domain.Crypto
{
    public class Security
    {
        public static string Encrypt(string password)
        {
            return Crypt.EnhancedHashPassword(password);
        }

        public static bool Validate(string password, string enhancedPassword) 
        {
            return Crypt.EnhancedVerify(password, enhancedPassword);
        }
    }
}
