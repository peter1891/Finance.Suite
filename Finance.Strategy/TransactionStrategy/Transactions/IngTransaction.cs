using Finance.Models;
using Finance.Repository.Interface.Models;
using Finance.Services.Authentication.Interface;
using Finance.Strategy.TransactionStrategy.Transactions.Interface;

namespace Finance.Strategy.TransactionStrategy.Transactions
{
    public class IngTransaction : ITransactionStrategy
    {
        private readonly ITransactionRepository _transactionRepository;

        public IngTransaction(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async void ImportTransactions(int accountId, string file)
        {
            // Import ING specific .csv file
            string[] csvfilerecord = File.ReadAllLines(file);

            foreach (var record in csvfilerecord.Skip(1))
            {
                if (!String.IsNullOrEmpty(record))
                {
                    var cells = record.Split(";");
                    TransactionModel transactionModel = new TransactionModel()
                    {
                        Date = DateTime.ParseExact(cells[0].Trim('"'), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                        Amount = double.Parse(cells[6].Trim('"')),
                        Type = cells[5].Trim('"'),
                        Name = cells[1].Trim('"'),
                        CounterParty = cells[3].Trim('"'),
                        Description = cells[8].Trim('"'),
                        AccountId = accountId,
                    };

                    if (await _transactionRepository.VerifyTransactionAsync(transactionModel))
                        await _transactionRepository.AddAsync(transactionModel);
                }
            }
        }
    }
}
