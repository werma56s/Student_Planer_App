using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SPlanner.Security
{
    public class PasswordSecurity
    {
        public static string ErrorMessage { get; private set; }

        //https://docs.microsoft.com/pl-pl/dotnet/api/system.security.cryptography.hashalgorithm.computehash?view=net-5.0
        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
        
        //check password
        public static bool CheckPassword(string text)
        {
            ErrorMessage = string.Empty;

            int lenghtPass = text.Length;
            if (lenghtPass >= 8 && lenghtPass <= 40)
            {
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasLowerChar = new Regex(@"[a-z]+");
                var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

                if (!hasLowerChar.IsMatch(text))
                {
                    ErrorMessage = "Password should contain At least one lower case letter for example: a, b, c..";
                    return false;
                }
                else if (!hasUpperChar.IsMatch(text))
                {
                    ErrorMessage = "Password should contain At least one upper case letter for example: A, B, C..";
                    return false;
                }
                else if (!hasNumber.IsMatch(text))
                {
                    ErrorMessage = "Password should contain At least one numeric value for example: 0, 1, 2, 3, 4..";
                    return false;
                }
                else if (!hasSymbols.IsMatch(text))
                {
                    ErrorMessage = "Password should contain At least one special case characters for example: $, #, @, !, %, ^, &, *, (,)";
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                ErrorMessage = "Password is too short or too long";
                return false;
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return password = PasswordSecurity.GetHash(sha256Hash, password);
            }
        }
    }
}