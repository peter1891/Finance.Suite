using System.Security;

namespace Finance.Utilities.Encoder.Interface
{
    public interface IPasswordEncoder
    {
        byte[] GetSalt();
        string GetHashPassword(SecureString secureString, byte[] salt);
    }
}
