using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class HashClass
    {
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {

                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }






        public static string BitConverterHasj(string inpur)
        {
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inpur));
                // Get the hashed string.  
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                // Print the string.   
                return hash;
            }
        }
    }
}
