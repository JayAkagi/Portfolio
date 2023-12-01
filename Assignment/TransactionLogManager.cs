using System.Collections.Generic;

namespace Assignment
{
    public class TransactionLogManager
    {
        private List<TransactionLogEntry> transactions;

        public TransactionLogManager()
        {
            this.transactions = new List<TransactionLogEntry>();
        }

        public void AddTransactionLog(TransactionLogEntry entry)
        {
            transactions.Add(entry);
        }

        public List<TransactionLogEntry> GetTransactionLog()
        {
            return transactions;
        }
    }
}
