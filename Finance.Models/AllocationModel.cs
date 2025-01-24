using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table("allocations")]
    public class AllocationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public double TargetAmount {  get; set; }
        public DateTime TargetDate { get; set; }

        public int AccountId { get; set; }
        public AccountModel Account { get; set; }

        public List<TransactionModel> Transactions { get; set; }
    }
}
