using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table("transactions")]
    public class TransactionModel
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }

        public int AccountId { get; set; }
        public AccountModel Account { get; set; }

        public string CounterParty { get; set; }

        public string Description { get; set; }
    }
}
