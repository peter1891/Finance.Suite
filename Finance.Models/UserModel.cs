using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table("users")]
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<AccountModel> Accounts { get; set; }
    }
}
