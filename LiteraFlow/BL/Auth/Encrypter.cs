using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LiteraFlow.Web.BL
{
    public static class Encrypter
    {
        public static string HashPassword(string sourcePswd, string Salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                sourcePswd, 
                System.Text.Encoding.ASCII.GetBytes(Salt), 
                KeyDerivationPrf.HMACSHA512, 
                5000, 
                64));

        }
    }
}
