using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace Finance.Models
{
    [Table("users")]
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<AccountModel> Accounts { get; set; }
    }
}
