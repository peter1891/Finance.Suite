using Finance.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table("accounts")]
    public class AccountModel
    {
        public int Id { get; set; }
        public BankName Bank { get; set; }
        public string AccountNumber { get; set; }
        public string Owner { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public List<TransactionModel> Transactions { get; set; }
    }
}
