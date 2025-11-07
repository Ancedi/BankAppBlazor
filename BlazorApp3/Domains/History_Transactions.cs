namespace BankAppBlazor.Domains
{
    public class Transaction
    {
        public Guid ID { get; set; } = Guid.NewGuid(); //Property to differentiate identical transactions.
        public DateTime Time = DateTime.Now;
        public TransactionType TransactionType { get; set; } //property for options of transactions
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; } //Property to show updated balance.
        public Guid? Receiver { get; set; } //FromAccountID
        public Guid? Sender { get; set; } //ToAccountID
        public decimal _inputAmountFilter { get; set; } //Added variable for filter implemented in TransactionHistory.
    }
    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        TransferIn,
        TransferOut
    }
}