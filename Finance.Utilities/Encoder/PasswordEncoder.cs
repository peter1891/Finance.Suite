using Finance.Utilities.Encoder.Interface;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Finance.Utilities.Encoder
{
    public class PasswordEncoder : IPasswordEncoder
    {
        public string GetHashPassword(SecureString secureString)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                string password = Marshal.PtrToStringUni(ptr);

                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

                byte[] salt = System.Text.Encoding.UTF8.GetBytes("2PluT0!");

                byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, passwordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, salt.Length, passwordBytes.Length);

                using (var sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(passwordWithSalt);

                    return Convert.ToBase64String(hashBytes);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }
    }
}
