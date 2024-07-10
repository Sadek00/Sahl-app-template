using AppTemplate.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Utilities
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string securityStamp);
        bool VerifyPassword(string password, string hashedPassword, string securityStamp);
    }

    public class PasswordHasher : IPasswordHasher
    {
        /*
        public static string HashPassword(string password, string securityStamp)
        {
            var stampedPassword = password + securityStamp;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stampedPassword));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword, string securityStamp)
        {
            string hashedInput = HashPassword(password, securityStamp);
            return hashedInput == hashedPassword;
        }
        */
        public string HashPassword(string password, string securityStamp)
        {
            var stampedPassword = password + securityStamp;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stampedPassword));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword, string securityStamp)
        {
            string hashedInput = HashPassword(password, securityStamp);
            return hashedInput == hashedPassword;
        }
    }
}
