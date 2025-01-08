using System.Security;

namespace Finance.Utilities.Encoder.Interface
{
    public interface IPasswordEncoder
    {
        string GetHashPassword(SecureString secureString);
    }
}
