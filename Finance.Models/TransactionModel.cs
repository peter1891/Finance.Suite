using Finance.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table("transactions")]
    public class TransactionModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public TransactionType Type { get; set; }

        public string Name { get; set; }
        public string CounterParty { get; set; }

        public string Description { get; set; }

        public bool Recurring { get; set; }
        public string? Comment { get; set; }

        public int AccountId { get; set; }
        public AccountModel Account { get; set; }
    }
}
